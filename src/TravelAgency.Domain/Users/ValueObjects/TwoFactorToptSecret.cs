using TravelAgency.Domain.Common.Errors;
using System.Text;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Results;
using static TravelAgency.Domain.Common.Utilities.ListUtilities;
using static TravelAgency.Domain.Users.Errors.DomainErrors;
using TravelAgency.Domain.Common.Utilities;

namespace TravelAgency.Domain.Users.ValueObjects;

public sealed class TwoFactorToptSecret : ValueObject
{
    public const int BytesLong = 32;

    private TwoFactorToptSecret(string value)
    {
        Value = value;
    }

    public new string Value { get; }

    public static ValidationResult<TwoFactorToptSecret> Create(string twoFactorToptSecret)
    {
        var errors = Validate(twoFactorToptSecret);
        return errors.CreateValidationResult(() => new TwoFactorToptSecret(twoFactorToptSecret));
    }

    public static IList<Error> Validate(string twoFactorTokenHash)
    {
        return EmptyList<Error>()
            .If(twoFactorTokenHash.IsNullOrEmptyOrWhiteSpace(), TwoFactorToptSecretError.Empty)
            .If(Encoding.ASCII.GetByteCount(twoFactorTokenHash) != BytesLong, TwoFactorToptSecretError.BytesLong);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}