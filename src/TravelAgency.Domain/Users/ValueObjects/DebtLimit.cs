using TravelAgency.Domain.Billing.ValueObjects;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Results;
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

    public bool IsCanUseDebt(Money price)
    {
        if(Value <= 0)
            return false;

        return price >= -Value;
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
