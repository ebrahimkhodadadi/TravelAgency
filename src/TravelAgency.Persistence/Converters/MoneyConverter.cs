using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgency.Domain.Billing.ValueObjects;

namespace TravelAgency.Persistence.Converters;

public sealed class MoneyConverter : ValueConverter<Money, int>
{
    public MoneyConverter() : base(money => money.Value, @int => Money.Create(@int).Value) { }
}

public sealed class MoneyComparer : ValueComparer<Money>
{
    public MoneyComparer() : base((money1, money2) => money1!.Value == money2!.Value, discount => discount.GetHashCode()) { }
}
