using TravelAgency.Domain.Common.Errors;

namespace TravelAgency.Persistence.Exceptions;

[Serializable]
public sealed class MigrationException(Error error) : Exception(error.Message)
{
}