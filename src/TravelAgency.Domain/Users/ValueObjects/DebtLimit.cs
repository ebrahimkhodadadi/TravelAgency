using System.Text.RegularExpressions;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Common.Utilities;
using TravelAgency.Domain.Common.Errors;
using static TravelAgency.Domain.Users.Errors.DomainErrors;
using static TravelAgency.Domain.Common.Utilities.ListUtilities;

namespace TravelAgency.Domain.Users.ValueObjects;

public sealed class DebtLimit : ValueObject
{
    public new int Value { get; }
    private DebtLimit(int value)
    {
        Value = value;
    }

    public static ValidationResult<DebtLimit> Create(int debtLimit)
    {
        var errors = Validate(debtLimit);
        return errors.CreateValidationResult(() => new DebtLimit(debtLimit));
    }

    public static IList<Error> Validate(int debtLimit)
    {
        return EmptyList<Error>();
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
