using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Billing
{
    public readonly record struct DiscountLogId : IEntityId<DiscountLogId>
    {
        private DiscountLogId(Ulid id)
        {
            Value = id;
        }

        public Ulid Value { get; }

        public static DiscountLogId New()
        {
            return new DiscountLogId(Ulid.NewUlid());
        }

        public static DiscountLogId Create(Ulid id)
        {
            return new DiscountLogId(id);
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

        public static bool operator >(DiscountLogId a, DiscountLogId b) => a.CompareTo(b) is 1;
        public static bool operator <(DiscountLogId a, DiscountLogId b) => a.CompareTo(b) is -1;
        public static bool operator >=(DiscountLogId a, DiscountLogId b) => a.CompareTo(b) >= 0;
        public static bool operator <=(DiscountLogId a, DiscountLogId b) => a.CompareTo(b) <= 0;
    }
}
