using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgency.Domain.Billing.Enumerations;
using TravelAgency.Domain.Users.Enumerations;

namespace TravelAgency.Persistence.Converters.Enums;

public sealed class TravelTypeConverter : ValueConverter<TravelType, string>
{
public TravelTypeConverter() : base(travelType => travelType.ToString(), @string => (TravelType)Enum.Parse(typeof(TravelType), @string)) { }
}

