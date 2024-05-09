using TravelAgency.Domain.Billing.ValueObjects;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Billing;

public sealed class Payment : Entity<PaymentId>, IAuditable
{
    public Money Money { get; private set; }

    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
