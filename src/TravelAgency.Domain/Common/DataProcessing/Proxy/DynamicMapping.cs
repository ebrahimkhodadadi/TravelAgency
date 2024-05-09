using TravelAgency.Domain.Common.BaseTypes.Abstractions;
using TravelAgency.Domain.Common.DataProcessing.Abstractions;

namespace TravelAgency.Domain.Common.DataProcessing.Proxy;

public sealed class DynamicMapping : IDynamicMapping
{
    public static IReadOnlyCollection<string> AllowedProperties => [];

    public IList<MappingEntry> MappingEntries { get; init; } = [];

    public TDynamicMapping To<TDynamicMapping, TEntity>()
        where TDynamicMapping : class, IDynamicMapping<TEntity>, new()
        where TEntity : class, IEntity
    {
        return new TDynamicMapping()
        {
            MappingEntries = MappingEntries
        };
    }
}