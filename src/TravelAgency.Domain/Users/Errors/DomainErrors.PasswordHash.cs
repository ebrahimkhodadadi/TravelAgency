using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Users.ValueObjects;

namespace TravelAgency.Domain.Users.Errors;

public static partial class DomainErrors
{
    public static class PasswordHashError
    {
        public static readonly Error Empty = Error.New(
            $"{nameof(PasswordHash)}.{nameof(Empty)}",
            $"{nameof(PasswordHash)} is empty.");

        public static readonly Error BytesLong = Error.New(
            $"{nameof(PasswordHash)}.{nameof(BytesLong)}",
            $"{nameof(PasswordHash)} needs to be less than {PasswordHash.BytesLong} bytes long.");
    }
}