using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Results;
using static TravelAgency.Domain.Common.Utilities.ListUtilities;
using static TravelAgency.Domain.Users.Errors.DomainErrors;
using TravelAgency.Domain.Common.Utilities;

namespace TravelAgency.Domain.Users.ValueObjects;

public sealed class LastName : ValueObject
{
    public const int MaxLength = 50;

    private LastName(string value)
    {
        Value = value;
    }

    public new string Value { get; }

    public static ValidationResult<LastName> Create(string lastName)
    {
        var errors = Validate(lastName);
        return errors.CreateValidationResult(() => new LastName(lastName));
    }

    public static IList<Error> Validate(string lastName)
    {
        return EmptyList<Error>()
            .If(lastName.IsNullOrEmptyOrWhiteSpace(), LastNameError.Empty)
            .If(lastName.Length > MaxLength, LastNameError.TooLong)
            .If(lastName.ContainsIllegalCharacter(), LastNameError.ContainsIllegalCharacter)
            .If(lastName.ContainsDigit(), LastNameError.ContainsDigit);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}