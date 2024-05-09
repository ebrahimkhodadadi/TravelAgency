using TravelAgency.Domain.Common.BaseTypes.Abstractions;
using TravelAgency.Domain.Common.DataProcessing.Abstractions;

namespace TravelAgency.Domain.Common.DataProcessing.Proxy;

public sealed class DynamicSortBy : IDynamicSortBy
{
    public static IReadOnlyCollection<string> AllowedSortProperties => [];

    public IList<SortByEntry> SortProperties { get; init; } = [];

    public TDynamicSortBy To<TDynamicSortBy, TEntity>()
        where TDynamicSortBy : class, IDynamicSortBy<TEntity>, new()
        where TEntity : class, IEntity
    {
        return new TDynamicSortBy()
        {
            SortProperties = SortProperties
        };
    }
}