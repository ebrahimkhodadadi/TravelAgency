using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TravelAgency.Persistence.Converters;

public sealed class UlidToStringConverter : ValueConverter<Ulid, string>
{
    public UlidToStringConverter()
        : base(x => x.ToString(), x => Ulid.Parse(x))
    {
    }
}