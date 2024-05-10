using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgency.Domain.Billing.Enumerations;

namespace TravelAgency.Persistence.Converters.Enums;

public sealed class PaymentTypeConverter : ValueConverter<PaymentType, string>
{
    public PaymentTypeConverter() : base(paymentType => paymentType.ToString(), @string => (PaymentType)Enum.Parse(typeof(PaymentType), @string)) { }
}
