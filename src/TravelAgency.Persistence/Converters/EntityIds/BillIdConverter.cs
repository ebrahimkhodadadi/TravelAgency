using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgency.Domain.Billing;

namespace TravelAgency.Persistence.Converters.EntityIds;

public sealed class BillIdConverter : ValueConverter<BillId, string>
{
    public BillIdConverter() : base(id => id.Value.ToString(), ulid => BillId.Create(Ulid.Parse(ulid))) { }
}

public sealed class BillIdComparer : ValueComparer<BillId>
{
    public BillIdComparer() : base((id1, id2) => id1!.Value == id2!.Value, id => id.Value.GetHashCode()) { }
}