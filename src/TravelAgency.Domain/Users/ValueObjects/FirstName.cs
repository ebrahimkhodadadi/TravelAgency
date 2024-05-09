using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Results;
using static TravelAgency.Domain.Common.Utilities.ListUtilities;
using static TravelAgency.Domain.Users.Errors.DomainErrors;
using TravelAgency.Domain.Common.Utilities;

namespace TravelAgency.Domain.Users.ValueObjects;

public sealed class FirstName : ValueObject
{
    public const int MaxLength = 50;

    private FirstName(string value)
    {
        Value = value;
    }

    public new string Value { get; }

    public static ValidationResult<FirstName> Create(string firstName)
    {
        var errors = Validate(firstName);
        return errors.CreateValidationResult(() => new FirstName(firstName));
    }

    public static IList<Error> Validate(string firstName)
    {
        return EmptyList<Error>()
            .If(firstName.IsNullOrEmptyOrWhiteSpace(), FirstNameError.Empty)
            .If(firstName.Length > MaxLength, FirstNameError.TooLong)
            .If(firstName.ContainsIllegalCharacter(), FirstNameError.ContainsIllegalCharacter)
            .If(firstName.ContainsDigit(), FirstNameError.ContainsDigit);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
