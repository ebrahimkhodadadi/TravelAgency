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

public sealed class Email : ValueObject
{
    public const int MaxLength = 40;

    private static readonly Regex _regex = new(@"^([a-zA-Z])([a-zA-Z0-9]+)\.?([a-zA-Z0-9]+)@([a-z]+)\.[a-z]{2,3}$", Compiled | CultureInvariant | Singleline, TimeSpan.FromMilliseconds(100));

    public new string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static ValidationResult<Email> Create(string email)
    {
        var errors = Validate(email);
        return errors.CreateValidationResult(() => new Email(email));
    }

    public static IList<Error> Validate(string email)
    {
        return EmptyList<Error>()
            .If(email.IsNullOrEmptyOrWhiteSpace(), EmailError.Empty)
            .If(email.Length > MaxLength, EmailError.TooLong)
            .If(_regex.NotMatch(email), EmailError.Invalid);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
