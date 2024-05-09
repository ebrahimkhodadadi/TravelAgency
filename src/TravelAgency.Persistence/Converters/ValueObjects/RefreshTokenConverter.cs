using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgency.Domain.Users.ValueObjects;

namespace TravelAgency.Persistence.Converters.ValueObjects;

public sealed class RefreshTokenConverter : ValueConverter<RefreshToken, string>
{
    public RefreshTokenConverter() : base(refreshToken => refreshToken.Value, @string => RefreshToken.Create(@string).Value) { }
}

public sealed class RefreshTokenComparer : ValueComparer<RefreshToken>
{
    public RefreshTokenComparer() : base((refreshToken1, refreshToken2) => refreshToken1!.Value == refreshToken2!.Value, passwordHash => passwordHash.GetHashCode()) { }
}