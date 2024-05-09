using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgency.Domain.Users.Enumerations;

namespace TravelAgency.Persistence.Converters.Enums;

public sealed class GenderConverter : ValueConverter<Gender, string>
{
    public GenderConverter() : base(gender => gender.ToString(), @string => (Gender)Enum.Parse(typeof(Gender), @string)) { }
}
