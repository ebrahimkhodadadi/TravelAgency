using TravelAgency.Domain.Common.Errors;
using System.Text;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Results;
using static TravelAgency.Domain.Common.Utilities.ListUtilities;
using static TravelAgency.Domain.Users.Errors.DomainErrors;
using TravelAgency.Domain.Common.Utilities;

namespace TravelAgency.Domain.Users.ValueObjects;

public sealed class TwoFactorTokenHash : ValueObject
{
    public const int NotHashedTokenFirstPartLength = 5;
    public const char NotHashedTokenSeparator = '-';
    public const int NotHashedTokenSecondPartLength = 5;

    public const int BytesLong = 514;

    private TwoFactorTokenHash(string value)
    {
        Value = value;
    }

    public new string Value { get; }

    public static ValidationResult<TwoFactorTokenHash> Create(string twoFactorTokenHash)
    {
        var errors = Validate(twoFactorTokenHash);
        return errors.CreateValidationResult(() => new TwoFactorTokenHash(twoFactorTokenHash));
    }

    public static IList<Error> Validate(string twoFactorTokenHash)
    {
        return EmptyList<Error>()
            .If(twoFactorTokenHash.IsNullOrEmptyOrWhiteSpace(), TwoFactorTokenError.Empty)
            .If(Encoding.ASCII.GetByteCount(twoFactorTokenHash) > BytesLong, TwoFactorTokenError.BytesLong);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}