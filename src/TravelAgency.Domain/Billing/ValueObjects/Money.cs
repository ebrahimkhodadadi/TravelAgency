using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Results;
using static TravelAgency.Domain.Billing.Errors.DomainErrors;
using static TravelAgency.Domain.Common.Utilities.ListUtilities;

namespace TravelAgency.Domain.Billing.ValueObjects;

public sealed class Money : ValueObject
{
    public new int Value { get; }
    public const int MinAmount = 1;

    public Money(int value)
    {
        Value = value;
    }
    public static ValidationResult<Money> Create(int amount)
    {
        var errors = Validate(amount);
        return errors.CreateValidationResult(() => new Money(amount));
    }

    public static IList<Error> Validate(int amount)
    {
        return EmptyList<Error>();
            //.If(amount < MinAmount, AmountError.TooLow);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static implicit operator Money(int value)
    {
        return new Money(value);
    }

    public static Money operator +(Money left, Money right)
    {
        var finalValue = left.Value + right.Value;
        return new Money(finalValue);
    }

    public static Money operator -(Money left, Money right)
    {
        var finalValue = left.Value - right.Value;
        return new Money(finalValue);
    }

    public static Money operator *(Money left, int right)
    {
        var finalValue = left.Value * right;
        return new Money(finalValue);
    }

    public static Money operator /(Money left, int right)
    {
        var finalValue = left.Value / right;
        return new Money(finalValue);
    }

    public Money ConvertToRial()
    {
        var rialValue = Value * 10;
        return new Money(rialValue);
    }

    public Money ConvertToToman()
    {
        var tomanValue = Value / 10;
        return new Money(tomanValue);
    }

    public static bool operator >(Money left, Money right) =>
        left.Value > right.Value;

    public static bool operator <(Money left, Money right) =>
        left.Value < right.Value;

    public static bool operator >=(Money left, Money right) =>
        left?.Value >= right?.Value;

    public static bool operator <=(Money left, Money right) =>
        left?.Value <= right?.Value;

    public bool IsValueZero() =>
        Value <= 0;

    public override string ToString() => "تومان " + Value.ToString("N0");
}
