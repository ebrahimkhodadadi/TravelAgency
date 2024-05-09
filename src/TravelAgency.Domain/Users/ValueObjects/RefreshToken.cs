using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Results;
using static TravelAgency.Domain.Common.Utilities.ListUtilities;
using static TravelAgency.Domain.Users.Errors.DomainErrors;
using TravelAgency.Domain.Common.Utilities;

namespace TravelAgency.Domain.Users.ValueObjects;

public sealed class RefreshToken : ValueObject
{
    public const int Length = 32;

    private RefreshToken(string value)
    {
        Value = value;
    }

    public new string Value { get; }

    public static ValidationResult<RefreshToken> Create(string refreshToken)
    {
        var errors = Validate(refreshToken);
        return errors.CreateValidationResult(() => new RefreshToken(refreshToken));
    }

    public static IList<Error> Validate(string refreshToken)
    {
        return EmptyList<Error>()
            .If(refreshToken.IsNullOrEmptyOrWhiteSpace(), RefreshTokenError.Empty)
            .If(refreshToken.Length != Length, RefreshTokenError.InvalidLength);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
