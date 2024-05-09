using TravelAgency.Domain.Common.BaseTypes;

namespace TravelAgency.Domain.Billing.ValueObjects;

public sealed class Direction : ValueObject
{
    public string Origin { get; } // مبدا
    public string Destination { get; } // مقصد

    public Direction(string origin, string destination)
    {
        if (string.IsNullOrWhiteSpace(origin))
            throw new ArgumentNullException(nameof(origin), "Origin cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(destination))
            throw new ArgumentNullException(nameof(destination), "Destination cannot be null or empty.");

        Origin = origin;
        Destination = destination;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Origin;
        yield return Destination;
    }
}
