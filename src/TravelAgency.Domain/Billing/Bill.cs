using TravelAgency.Domain.Billing.Enumerations;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Users;
using static TravelAgency.Domain.Billing.Errors.DomainErrors.Status;
using static TravelAgency.Domain.Billing.Enumerations.BillStatus;

namespace TravelAgency.Domain.Billing;

public sealed class Bill : AggregateRoot<BillId>, IAuditable, ISoftDeletable
{


    public BillStatus Status { get; private set; }

    private readonly List<Travel> _travels = [];
    public IReadOnlyCollection<Travel> Travels => _travels.AsReadOnly();

    private readonly List<Payment> _payments = [];
    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

    public UserId UserId { get; private set; }

    public DateTimeOffset? SoftDeletedOn { get; set; }
    public bool SoftDeleted { get; set; }

    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public Bill()
    {
        //if(Status is Closed || Status is Canceled)
        //    return Result.Failure(Errors.DomainErrors.Status.BillClosed);
    }

    public void SoftDelete()
    {
        SoftDeleted = true;
        SoftDeletedOn = DateTimeOffset.UtcNow;
    }

    public Result Close(BillStatus newBillStatus)
    {
        if (Status.CanBeChangedTo(newBillStatus) is false)
            return Result.Failure(InvalidStatusChange(Status, newBillStatus));

        if(Payments.Sum(x => x.Money.Value) != Travels.Sum(x => x.Money.Value))
            return Result.Failure(Errors.DomainErrors.TotalPrice.SameTotalPrice);

        Status = newBillStatus;
        return Result.Success();
    }
}
