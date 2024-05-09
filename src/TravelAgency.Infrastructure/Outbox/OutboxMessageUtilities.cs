using Newtonsoft.Json;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Infrastructure.Outbox;

public static class OutboxMessageUtilities
{
    public static IDomainEvent? Deserialize(this OutboxMessage outboxMessage, TypeNameHandling typeNameHandling = TypeNameHandling.None)
    {
        if (outboxMessage is null)
        {
            return null;
        }

        return JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content, new JsonSerializerSettings
        {
            TypeNameHandling = typeNameHandling
        });
    }
}