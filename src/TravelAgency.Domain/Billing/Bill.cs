using TravelAgency.Domain.Billing.Enumerations;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Users;
using static TravelAgency.Domain.Billing.Errors.DomainErrors.BillStatus;
using static TravelAgency.Domain.Billing.Enumerations.BillStatus;
using static TravelAgency.Domain.Users.Enumerations.Rank;
using static TravelAgency.Domain.Common.Utilities.ListUtilities;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Billing.ValueObjects;
using TravelAgency.Domain.Billing.Services;
using TravelAgency.Domain.Billing.Repositories;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Domain.Billing;

/// <summary>
/// صورت حساب
/// </summary>
public sealed class Bill : AggregateRoot<BillId>, IAuditable, ISoftDeletable
{
    public Money Balance { get { return _payments.Sum(x => x.Price.Value) - _travels.Sum(x => x.Price.Value); } }
    public BillStatus Status { get; private set; }

    private readonly List<Travel> _travels = [];
    public IReadOnlyCollection<Travel> Travels => _travels.AsReadOnly();

    private readonly List<Payment> _payments = [];
    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

    private readonly List<DiscountLog> _discounts = [];
    public IReadOnlyCollection<DiscountLog> DiscountLogs => _discounts.AsReadOnly();

    public CustomerId CustomerId { get; private set; }
    public Customer Customer { get; private set; }

    public DateTimeOffset? SoftDeletedOn { get; set; }
    public bool SoftDeleted { get; set; }

    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    private Bill(
        BillId id,
        CustomerId customerId
        )
    : base(id)
    {
        CustomerId = customerId;
        Status = InProgress;
    }

    public static Bill Create
        (
        CustomerId customerId
        )
    {
        var bill = new Bill
        (
            id: BillId.New(),
            customerId: customerId
        );

        //bill.RaiseDomainEvent(billCreatedDomainEvent.New(bill.Id));

        return bill;
    }

    public void SoftDelete()
    {
        SoftDeleted = true;
        SoftDeletedOn = DateTimeOffset.UtcNow;
    }

    // پرداخت
    public Result<Payment> Pay(Money price, PaymentType paymentType)
    {
        if (Status is Closed || Status is Canceled)
            return Result.Failure<Payment>(Errors.DomainErrors.BillStatus.BillClosed);
        if(Customer.Rank is Cash && paymentType is PaymentType.Cheque)
            return Result.Failure<Payment>(Errors.DomainErrors.PaymentStatus.CashCustomer);

        var fee = CalculateFee();
        var discount = CalculateDiscount();
        var totalPrice = price + fee - discount;

        var payment = Payment.Create(PaymentId.New(), totalPrice, paymentType, Id, "پرداخت");
        _payments.Add(payment);

        return Result.Success(payment);
    }
    private Money CalculateFee()
    {
        var dailyBalanceFee = CalculateDailyBalance(DateTime.Now.AddDays(-10), DateTime.Now);
        return CreditService.CalculateCreditUsageFee(Customer.Rank, dailyBalanceFee.Item1, dailyBalanceFee.Item3);
    }
    private Money CalculateDiscount()
    {
        Money discount = 0;
        if (!_discounts.Any(x => x.CreatedOn > DateTime.Now.AddDays(-30)))
        {
            var dailyBalanceDiscount = CalculateDailyBalance(DateTime.Now.AddDays(-30), DateTime.Now);
            discount = DiscountService.CalculateGoodPayerDiscount(dailyBalanceDiscount.Item1, dailyBalanceDiscount.Item2);
            if (!discount.IsValueZero())
                _discounts.Add(DiscountLog.Create(Id, discount));
        }

        return discount;
    }

    // محاسبه بالانس روزانه
    internal (Dictionary<DateTime, Money>, Money, Money) CalculateDailyBalance(DateTime billStartDate, DateTime billEndDate)
    {
        List<Payment> transactions = Payments
            .Where(t => t.CreatedOn >= billStartDate && t.CreatedOn <= billEndDate)
            .OrderBy(t => t.CreatedOn)
            .ToList();
        List<Travel> travels = Travels
            .Where(t => t.CreatedOn >= billStartDate && t.CreatedOn <= billEndDate)
            .OrderBy(t => t.CreatedOn)
            .ToList();

        Dictionary<DateTime, Money> dailyBalances = new Dictionary<DateTime, Money>();
        foreach (Payment transaction in transactions)
        {
            DateTime transactionDate = transaction.CreatedOn.DateTime.Date;
            Money dailyBalance = transaction.Price;

            if (dailyBalances.ContainsKey(transactionDate))
            {
                dailyBalances[transactionDate] += dailyBalance;
            }
            else
            {
                dailyBalances[transactionDate] = dailyBalance;
            }
        }
        foreach (Travel travel in travels)
        {
            DateTime transactionDate = travel.CreatedOn.DateTime.Date;
            Money dailyBalance = travel.Price;

            if (dailyBalances.ContainsKey(transactionDate))
            {
                dailyBalances[transactionDate] -= dailyBalance;
            }
            else
            {
                dailyBalances[transactionDate] = -dailyBalance.Value;
            }
        }

        Money totalBalance = dailyBalances.Sum(kv => kv.Value.Value);
        int numberOfDays = dailyBalances.Keys.Count();
        Money averageDailyBalance = totalBalance / numberOfDays;

        return (dailyBalances, totalBalance, averageDailyBalance);
    }

    // بستن صورت حساب
    public Result Close()
    {
        if (Status.CanBeChangedTo(BillStatus.Closed) is false)
            return Result.Failure(InvalidStatusChange(Status, BillStatus.Closed));

        if (Payments.Sum(x => x.Price.Value) != Travels.Sum(x => x.Price.Value))
            return Result.Failure(Errors.DomainErrors.Price.SameTotalPrice);

        Status = BillStatus.Closed;
        return Result.Success();
    }

    public Result CreateTravel(Travel travel)
    {
        if (Status is Closed || Status is Canceled)
            return Result.Failure(Errors.DomainErrors.BillStatus.BillClosed);

        _travels.Add(travel);

        //مشتری های نقدی اجازه ثبت پرداخت اسنادی را ندارند و هیچ گاه نباید بالانس صورت حساب آنان )
        //مجموع سفرها منهای مجموع پرداخت ها( بیشتر از صفر شود )یعنی هیچ وقت نباید به ما بدهکار باشند
        if (Customer.Rank == Cash && Travels.Sum(x => x.Price.Value) != Payments.Sum(x => x.Price.Value))
            return Result.Failure(Errors.DomainErrors.Price.MustPayBeforeCreateTravel(travel.Price));
        // مشتریان اعتباری می توانند پرداخت های اسنادی ثبت کنند و حتی تا سقف مشخصی که
        // برای هر کدام به صورت جداگانه تعیین می شود می توانند به ما بدهکار باشند )بالانس صورت حسابشان بیشتر ازصفر شود(.
        if (Customer.Rank == Credit)
        {
            if (!Customer.DebtLimit.IsCanUseDebt(Balance))
                return Result.Failure(Errors.DomainErrors.Price.MustUseLessThanCreditLimit);
            //مجموع پرداخت های اسنادی این مشتریان هیچ گاه نباید از ۵۰ درصد کل مبلغ صورت حساب بیشترشود.
            var checkAmount = Payments.Where(x => x.PaymentType == PaymentType.Cheque).Sum(x => x.Price.Value);
            var paymentAmount = (Payments.Sum(x => x.Price.Value) * 50) / 100;
            if (checkAmount > paymentAmount)
                return Result.Failure(Errors.DomainErrors.Price.MustHaveLessThanSumCheque);
        }

        return Result.Success();
    }

    private Result<Payment> CreatePayment(Payment payment)
    {
        if (Status is Closed || Status is Canceled)
            return Result.Failure<Payment>(Errors.DomainErrors.BillStatus.BillClosed);

        _payments.Add(payment);
        return Result.Success(payment);
    }

    public async Task<Result> TransferPayment(Money price, BillId to, IBillRepository _billRepository)
    {
        if((Balance - price) < 0)
            return Result.Failure(Errors.DomainErrors.Price.NotEnoughBalance);

        var transferOldPay = CreatePayment(Payment.Create(PaymentId.New(), -price.Value, PaymentType.Transfer, Id)).Value;
        var transferNewPay = CreatePayment(Payment.Create(PaymentId.New(), price.Value, PaymentType.Transfer, to)).Value;

        transferNewPay.SetTransferId(transferOldPay.Id);
        transferOldPay.SetTransferId(transferNewPay.Id);

        _payments.Add(transferOldPay);
        var toBill = await _billRepository.GetByIdAsync(transferNewPay.BillId);
        if(toBill is null)
            return Result.Failure(Error.NotFound<Bill>(transferNewPay.BillId));

        var result = toBill.CreatePayment(transferNewPay);
        if (result.IsFailure)
            return result;

        return Result.Success();
    }
}