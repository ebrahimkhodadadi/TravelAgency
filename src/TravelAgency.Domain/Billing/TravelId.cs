using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Billing;

public readonly record struct TravelId : IEntityId<TravelId>
{
    private TravelId(Ulid id)
    {
        Value = id;
    }

    public Ulid Value { get; }

    public static TravelId New()
    {
        return new TravelId(Ulid.NewUlid());
    }

    public static TravelId Create(Ulid id)
    {
        return new TravelId(id);
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

        if (other is not PaymentId otherPaymentId)
        {
            throw new ArgumentNullException($"IEntity is not {GetType().FullName}");
        }

        return Value.CompareTo(otherPaymentId.Value);
    }

    public static bool operator >(TravelId a, TravelId b) => a.CompareTo(b) is 1;
    public static bool operator <(TravelId a, TravelId b) => a.CompareTo(b) is -1;
    public static bool operator >=(TravelId a, TravelId b) => a.CompareTo(b) >= 0;
    public static bool operator <=(TravelId a, TravelId b) => a.CompareTo(b) <= 0;
}