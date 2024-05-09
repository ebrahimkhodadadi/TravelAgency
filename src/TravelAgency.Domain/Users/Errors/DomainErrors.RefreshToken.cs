using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Users.ValueObjects;

namespace TravelAgency.Domain.Users.Errors;

public static partial class DomainErrors
{
    public static class RefreshTokenError
    {
        public static readonly Error Empty = Error.New(
            $"{nameof(RefreshToken)}.{nameof(Empty)}",
            $"{nameof(RefreshToken)} is empty.");

        public static readonly Error InvalidLength = Error.New(
            $"{nameof(RefreshToken)}.{nameof(InvalidLength)}",
            $"{nameof(RefreshToken)} length must be {RefreshToken.Length} characters.");
    }
}