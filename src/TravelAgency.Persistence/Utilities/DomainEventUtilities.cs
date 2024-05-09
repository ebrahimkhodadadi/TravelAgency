using Newtonsoft.Json;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Persistence.Utilities;

public static class DomainEventUtilities
{
    public static string Serialize(this IDomainEvent domainEvent, TypeNameHandling typeNameHandling = TypeNameHandling.None)
    {
        return JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings
        {
            TypeNameHandling = typeNameHandling
        });
    }
}