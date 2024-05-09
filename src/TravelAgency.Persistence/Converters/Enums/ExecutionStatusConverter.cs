using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgency.Infrastructure.Outbox;

namespace TravelAgency.Persistence.Converters.Enums;

public sealed class ExecutionStatusConverter : ValueConverter<ExecutionStatus, string>
{
    public ExecutionStatusConverter() : base(status => status.ToString(), @string => (ExecutionStatus)Enum.Parse(typeof(ExecutionStatus), @string)) { }
}
