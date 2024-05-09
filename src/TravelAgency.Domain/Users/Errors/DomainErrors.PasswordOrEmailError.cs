using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Users;
using TravelAgency.Domain.Users.ValueObjects;

namespace TravelAgency.Domain.Users.Errors;

public static partial class DomainErrors
{
    public static class PasswordOrEmailError
    {
        /// <summary>
        /// Create an Error describing that a password or an email are invalid
        /// </summary>
        public static readonly Error InvalidPasswordOrEmail = Error.New(
            $"{nameof(User)}.{nameof(InvalidPasswordOrEmail)}",
            $"Invalid {nameof(Password)} or {nameof(Email)}.");
    }
}