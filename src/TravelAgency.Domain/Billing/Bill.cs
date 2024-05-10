using TravelAgency.Domain.Billing.Enumerations;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Users;
using static TravelAgency.Domain.Billing.Errors.DomainErrors.BillStatus;
using static TravelAgency.Domain.Billing.Enumerations.BillStatus;
using static TravelAgency.Domain.Users.Enumerations.Rank;

namespace TravelAgency.Domain.Billing;

/// <summary>
/// صورت حساب
/// </summary>
public sealed class Bill : AggregateRoot<BillId>, IAuditable, ISoftDeletable
{
    public BillStatus Status { get; private set; }

    private readonly List<Travel> _travels = [];
    public IReadOnlyCollection<Travel> Travels => _travels.AsReadOnly();

    private readonly List<Payment> _payments = [];
    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

    public CustomerId CustomerId { get; private set; }
    public Customer Customer { get; private set; }

    public DateTimeOffset? SoftDeletedOn { get; set; }
    public bool SoftDeleted { get; set; }

    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public Bill()
    {
    }

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

    public Result CreateTravel(Travel travel, Payment? payment = null)
    {
        if (Status is Closed || Status is Canceled)
            return Result.Failure(Errors.DomainErrors.BillStatus.BillClosed);

        _travels.Add(travel);

        if (payment != null)
            _payments.Add(payment);

        //مشتری های نقدی اجازه ثبت پرداخت اسنادی را ندارند و هیچ گاه نباید بالانس صورت حساب آنان )
        //مجموع سفرها منهای مجموع پرداخت ها( بیشتر از صفر شود )یعنی هیچ وقت نباید به ما بدهکار باشند
        if (Customer.Rank == Cash && Travels.Sum(x => x.Price.Value) != Payments.Sum(x => x.Price.Value))
            return Result.Failure(Errors.DomainErrors.Price.MustPayBeforeCreateTravel(travel.Price));
        // مشتریان اعتباری می توانند پرداخت های اسنادی ثبت کنند و حتی تا سقف مشخصی که
        // برای هر کدام به صورت جداگانه تعیین می شود می توانند به ما بدهکار باشند )بالانس صورت حسابشان بیشتر ازصفر شود(.
        if (Customer.Rank == Credit)
        {
            if ((Travels.Sum(x => x.Price.Value) - Payments.Sum(x => x.Price.Value)) > Customer.DebtLimit.Value)
                return Result.Failure(Errors.DomainErrors.Price.MustUseLessThanCreditLimit);
            //مجموع پرداخت های اسنادی این مشتریان هیچ گاه نباید از ۵۰ درصد کل مبلغ صورت حساب بیشترشود.
            var checkAmount = Payments.Where(x => x.PaymentType == PaymentType.Cheque).Sum(x => x.Price.Value);
            var paymentAmount = (Payments.Sum(x => x.Price.Value) * 50) / 100;
            if (checkAmount > paymentAmount)
                return Result.Failure(Errors.DomainErrors.Price.MustHaveLessThanSumCheque);
        }

        return Result.Success();
    }

    public Result CreatePayment(Payment payment)
    {
        if (Status is Closed || Status is Canceled)
            return Result.Failure(Errors.DomainErrors.BillStatus.BillClosed);

        _payments.Add(payment);
        return Result.Success();
    }

    public Result TransferPayment(Payment from, Payment to, IBillRepository _billRepository)
    {
        if (-from.Price.Value != to.Price.Value && -to.Price.Value != from.Price.Value)
            return Result.Failure(Errors.DomainErrors.Price.MustNotEquelForCheque);

        from.SetTransferId(to.Id);
        to.SetTransferId(from.Id);

        _payments.Add(from);
        var toBill = _billRepository.FindById(to.BillId);
        var result = toBill.CreatePayment(to);
        if (result.IsFailure)
            return result;

        return Result.Success();
    }
}