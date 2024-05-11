using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Users.ValueObjects;

namespace TravelAgency.Domain.Users.Errors;

public static partial class DomainErrors
{
    public static class PhoneNumberError
    {
        public static readonly Error Empty = Error.New(
            $"{nameof(PhoneNumber)}.{nameof(Empty)}",
            $"{nameof(PhoneNumber)} is empty.");

        public static readonly Error Invalid = Error.New(
            $"{nameof(PhoneNumber)}.{nameof(Invalid)}",
            $"{nameof(PhoneNumber)} must consist of 11 digits.");

        public static readonly Error PhoneNumberAlreadyTaken = Error.New(
            $"{nameof(PhoneNumber)}.{nameof(PhoneNumberAlreadyTaken)}",
            $"{nameof(PhoneNumber)} is already taken.");
    }
}