using TravelAgency.Domain.Billing;

namespace TravelAgency.Persistence.Specifications.Bills;

internal class PaymentSpecification
{
    internal static partial class ById
    {
        internal static Specification<Payment, PaymentId> Create(PaymentId paymentId)
        {
            var specification = Specification<Payment, PaymentId>.New()
                .AddFilters(payment => payment.Id == paymentId);

            return specification;
        }
    }
}
