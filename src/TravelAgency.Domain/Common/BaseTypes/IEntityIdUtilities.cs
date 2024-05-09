using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Common.BaseTypes;

public static class IEntityIdUtilities
{
    public static IList<Ulid> GetUlids<TEntityId>(this IList<TEntityId> ids)
        where TEntityId : IEntityId
    {
        return ids.Select(x => x.Value).ToList();
    }
}