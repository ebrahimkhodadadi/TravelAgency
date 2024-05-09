using TravelAgency.Domain.Billing.Enumerations;
using TravelAgency.Domain.Billing.ValueObjects;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Billing;

public sealed class Travel : Entity<TravelId>, IAuditable
{
    public Direction Direction { get; private set; } // مسیر
    public DateTimeOffset Start { get; set; }
    public TravelType Type { get; private set; } // انواع سفر
    public Money Money { get; private set; } 

    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
