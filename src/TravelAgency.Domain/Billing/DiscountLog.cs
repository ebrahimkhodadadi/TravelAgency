using TravelAgency.Domain.Billing.ValueObjects;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Billing;

public sealed class DiscountLog : Entity<DiscountId>, IAuditable
{
    public BillId BillId { get; private set; }
    public Money Price { get; private set; }

    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    private DiscountLog(
        DiscountId id,
        BillId billId,
        Money price
        )
    : base(id)
    {
        BillId = billId;
        Price = price;
    }

    public static DiscountLog Create
        (
        BillId billId,
        Money price
        )
    {
        var bill = new DiscountLog
        (
            id: DiscountId.New(),
            billId: billId,
            price: price
        );

        return bill;
    }
}
