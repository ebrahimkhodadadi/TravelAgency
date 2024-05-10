using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Billing
{
    public readonly record struct DiscountId : IEntityId<DiscountId>
    {
        private DiscountId(Ulid id)
        {
            Value = id;
        }

        public Ulid Value { get; }

        public static DiscountId New()
        {
            return new DiscountId(Ulid.NewUlid());
        }

        public static DiscountId Create(Ulid id)
        {
            return new DiscountId(id);
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

        public static bool operator >(DiscountId a, DiscountId b) => a.CompareTo(b) is 1;
        public static bool operator <(DiscountId a, DiscountId b) => a.CompareTo(b) is -1;
        public static bool operator >=(DiscountId a, DiscountId b) => a.CompareTo(b) >= 0;
        public static bool operator <=(DiscountId a, DiscountId b) => a.CompareTo(b) <= 0;
    }
}
