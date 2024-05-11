using TravelAgency.Domain.Billing.Enumerations;
using TravelAgency.Domain.Billing.ValueObjects;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Billing;

public sealed class Payment : Entity<PaymentId>, IAuditable
{
    public Money Price { get; private set; }
    public BillId BillId { get; private set; }
    public PaymentId? TransferId { get; private set; }
    public PaymentType PaymentType { get; private set; }
    public string Description { get; private set; }

    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public Payment()
    {

    }

    private Payment
        (
        PaymentId id,
        Money price,
        BillId billId,
        string description = null
        )
    : base(id)
    {
        Price = price;
        BillId = billId;
        Description = description;
    }

    public static Payment Create(Money price, BillId billId, string description = null)
    {
        return new Payment
        (
            id: PaymentId.New(),
            price: price,
            billId: billId,
            description: description
        );
    }

    public void SetTransferId(PaymentId transferId) =>
        TransferId = transferId;
}
