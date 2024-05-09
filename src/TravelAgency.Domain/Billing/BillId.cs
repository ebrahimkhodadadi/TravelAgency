using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Billing;

public readonly record struct BillId : IEntityId<BillId>
{
    private BillId(Ulid id)
    {
        Value = id;
    }

    public Ulid Value { get; }

    public static BillId New()
    {
        return new BillId(Ulid.NewUlid());
    }

    public static BillId Create(Ulid id)
    {
        return new BillId(id);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public int CompareTo(IEntityId? other)
    {
        if (other is null)
        {
            return 1;
        }

        if (other is not BillId otherBillId)
        {
            throw new ArgumentNullException($"IEntity is not {GetType().FullName}");
        }

        return Value.CompareTo(otherBillId.Value);
    }

    public static bool operator >(BillId a, BillId b) => a.CompareTo(b) is 1;
    public static bool operator <(BillId a, BillId b) => a.CompareTo(b) is -1;
    public static bool operator >=(BillId a, BillId b) => a.CompareTo(b) >= 0;
    public static bool operator <=(BillId a, BillId b) => a.CompareTo(b) <= 0;
}