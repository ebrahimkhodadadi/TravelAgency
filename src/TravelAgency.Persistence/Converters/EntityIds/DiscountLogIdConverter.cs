using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgency.Domain.Billing;

namespace TravelAgency.Persistence.Converters.EntityIds
{
    public sealed class DiscountLogIdConverter : ValueConverter<DiscountLogId, string>
    {
        public DiscountLogIdConverter() : base(id => id.Value.ToString(), ulid => DiscountLogId.Create(Ulid.Parse(ulid))) { }
    }

    public sealed class DiscountLogIdComparer : ValueComparer<DiscountLogId>
    {
        public DiscountLogIdComparer() : base((id1, id2) => id1!.Value == id2!.Value, id => id.Value.GetHashCode()) { }
    }
}
