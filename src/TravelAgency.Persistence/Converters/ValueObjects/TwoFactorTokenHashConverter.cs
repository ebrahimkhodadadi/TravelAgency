using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgency.Domain.Users.ValueObjects;

namespace TravelAgency.Persistence.Converters.ValueObjects;

public sealed class TwoFactorTokenHashConverter : ValueConverter<TwoFactorTokenHash, string>
{
    public TwoFactorTokenHashConverter() : base(twoFactorToken => twoFactorToken.Value, @string => TwoFactorTokenHash.Create(@string).Value) { }
}

public sealed class TwoFactorTokenHashComparer : ValueComparer<TwoFactorTokenHash>
{
    public TwoFactorTokenHashComparer() : base((twoFactorToken1, twoFactorToken2) => twoFactorToken1!.Value == twoFactorToken2!.Value, twoFactorToken => twoFactorToken.GetHashCode()) { }
}