using TravelAgency.Domain.Billing.ValueObjects;
using TravelAgency.Domain.Common.Errors;

namespace TravelAgency.Domain.Billing.Errors;

public static partial class DomainErrors
{
    public static class AmountError
    {
        public static readonly Error TooLow = Error.New(
            $"{nameof(Money)}.{nameof(TooLow)}",
            $"{nameof(Money)} must be at least {Money.MinAmount}.");
    }
}