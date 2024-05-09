using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Common.DataProcessing.Abstractions;

public interface IDynamicFilter : IFilter
{
    IList<FilterByEntry> FilterProperties { get; init; }
    abstract static IReadOnlyCollection<string> AllowedFilterProperties { get; }
    abstract static IReadOnlyCollection<string> AllowedFilterOperations { get; }
}

public interface IDynamicFilter<TEntity> : IFilter<TEntity>, IDynamicFilter
    where TEntity : class, IEntity
{
}