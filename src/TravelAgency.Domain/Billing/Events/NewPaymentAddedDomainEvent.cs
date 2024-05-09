using TravelAgency.Domain.Common.BaseTypes;

namespace TravelAgency.Domain.Billing.Events;
public sealed record NewPaymentAddedDomainEvent(Ulid Id) : DomainEvent(Id)
{
    public static NewPaymentAddedDomainEvent New()
    {
        return new NewPaymentAddedDomainEvent(Ulid.NewUlid());
    }
}
