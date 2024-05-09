using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Results;
using static TravelAgency.Domain.Common.Utilities.ListUtilities;
using static TravelAgency.Domain.Users.Errors.DomainErrors;
using TravelAgency.Domain.Common.Utilities;

namespace TravelAgency.Domain.Users.ValueObjects;

public sealed class Username : ValueObject
{
    public const int MaxLength = 30;

    private Username(string value)
    {
        Value = value;
    }

    public new string Value { get; }

    public static ValidationResult<Username> Create(string username)
    {
        var errors = Validate(username);
        return errors.CreateValidationResult(() => new Username(username));
    }

    public static IList<Error> Validate(string username)
    {
        return EmptyList<Error>()
            .If(username.IsNullOrEmptyOrWhiteSpace(), UsernameError.Empty)
            .If(username.Length > MaxLength, UsernameError.TooLong)
            .If(username.ContainsIllegalCharacter(), UsernameError.ContainsIllegalCharacter);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
