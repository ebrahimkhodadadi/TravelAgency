using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgency.Domain.Billing.Enumerations;
using TravelAgency.Domain.Users.Enumerations;

namespace TravelAgency.Persistence.Converters.Enums;

public sealed class BillStatusConverter : ValueConverter<BillStatus, string>
{
    public BillStatusConverter() : base(billStatus => billStatus.ToString(), @string => (BillStatus)Enum.Parse(typeof(BillStatus), @string)) { }
}
