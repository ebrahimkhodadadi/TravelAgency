using TravelAgency.Domain.Common.BaseTypes;

namespace TravelAgency.Domain.Users.ValueObjects;

public sealed class DebtLimit : ValueObject
{
    public new int Value { get; }
    public DebtLimit(int value)
    {
        Value = value;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
