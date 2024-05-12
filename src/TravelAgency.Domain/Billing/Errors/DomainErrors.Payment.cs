using TravelAgency.Domain.Common.Errors;

namespace TravelAgency.Domain.Billing.Errors;

public static partial class DomainErrors
{
    public static class PaymentStatus
    {
        public static readonly Error CashCustomer =
            Error.New($"{nameof(PaymentStatus)}.{nameof(CashCustomer)}",
            $"Cash Customers can't use Cheque payment Type.");
    }
}
