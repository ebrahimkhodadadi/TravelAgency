using TravelAgency.Domain.Common.Errors;
using System.Text.RegularExpressions;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Common.Utilities;
using static TravelAgency.Domain.Common.Utilities.ListUtilities;
using static TravelAgency.Domain.Users.Errors.DomainErrors;
using static System.Text.RegularExpressions.RegexOptions;

namespace TravelAgency.Domain.Users.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
    private static readonly Regex _regex = new(@"^(?:(?:(?:\\+?|00)(98))|(0))?((?:90|91|92|93|99)[0-9]{8})$", Compiled | CultureInvariant | Singleline, TimeSpan.FromMilliseconds(100));
    public new string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static ValidationResult<PhoneNumber> Create(string number)
    {
        var errors = Validate(number);
        return errors.CreateValidationResult(() => new PhoneNumber(number));
    }

    public static IList<Error> Validate(string number)
    {
        return EmptyList<Error>()
            .If(number.IsNullOrEmptyOrWhiteSpace(), PhoneNumberError.Empty)
            .If(_regex.NotMatch(number), PhoneNumberError.Invalid);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
