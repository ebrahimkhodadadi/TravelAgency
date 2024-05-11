using TravelAgency.Domain.Billing.Enumerations;
using TravelAgency.Domain.Billing.ValueObjects;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;
using TravelAgency.Domain.Common.Results;
using static TravelAgency.Domain.Billing.Errors.DomainErrors.TravelStatus;
using static TravelAgency.Domain.Billing.Enumerations.TravelStatus;
using static TravelAgency.Domain.Billing.Enumerations.TravelType;
using static TravelAgency.Domain.Common.Utilities.ListUtilities;
using TravelAgency.Domain.Common.Errors;

namespace TravelAgency.Domain.Billing;

public sealed class Travel : Entity<TravelId>, IAuditable
{
    public Direction Direction { get; private set; } // مسیر
    public DateTimeOffset Start { get; set; }
    public TravelType Type { get; private set; } // انواع سفر
    public TravelStatus Status { get; private set; } // وضعیت سفر
    public BillId BillId { get; private set; }
    public Money Price { get; private set; }

    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    private readonly List<Payment> _payments = [];
    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

    public Result Cancel()
    {
        var errors = EmptyList<Error>()
            .If(Status is Canceled, Error.InvalidOperation("سفر از قبل کنسل شده است"));
        if (errors.NotNullOrEmpty())
            return ValidationResult.WithErrors(errors);

        var fee = CalculateCancellationFee(Type, Start, Price);
        if (fee.IsFailure)
            return fee;
        _payments.Add(Payment.Create(-fee.Value.Value, BillId, "جریمه کنسلی"));

        return Result.Success();
    }
    // جریمه کنسلی
    public Result<Money> CalculateCancellationFee(TravelType type, DateTimeOffset departureTime, Money travelPrice)
    {
        decimal cancellationFeePercentage = 0;

        // Determine cancellation fee percentage based on trip type and departure time
        if (type is Flight)
        {
            //سفرهای هواپیما تا ساعت ۱۲ ظهر سه روز قبل از حرکت با ۳۰ درصد
            if (departureTime >= DateTime.Today.AddDays(3).Date && departureTime.Hour < 12)
                cancellationFeePercentage = 0.3m;
            // تا ساعت ۱۲ ظهر یک روز قبل از حرکت با ۶۰ درصد
            else if (departureTime >= DateTime.Today.AddDays(1).Date && departureTime.Hour < 12)
                cancellationFeePercentage = 0.6m;
            //تا نیم ساعت قبل از حرکت با ۸۰ درصد
            else if (departureTime >= DateTime.Now.AddHours(0.5))
                cancellationFeePercentage = 0.8m;
            // و پس از حرکت )با فرضاین که از پرواز استفاده نشده باشد( با ۹۰ درصد مبلغ بلیط قابل کنسل هستند
            else
                cancellationFeePercentage = 0.9m;
        }
        else if (type is Train || type is Bus)
        {
            //ساعت قبل از حرکت با ۱۰ درصد 2
            if (departureTime >= DateTime.Now.AddHours(2))
                cancellationFeePercentage = 0.1m;
            // بعد از آن با ۵۰ درصد جریمه قابل کنسل خواهند بود
            else if(type != Train)
                cancellationFeePercentage = 0.5m;
            else
                return Result.Failure<Money>(Errors.DomainErrors.TravelStatus.CancelTrain);
        }

        // Calculate cancellation fee based on total amount
        Money cancellationFee = (int)Math.Round(cancellationFeePercentage * travelPrice.Value);
        return Result.Success(cancellationFee);
    }
}
