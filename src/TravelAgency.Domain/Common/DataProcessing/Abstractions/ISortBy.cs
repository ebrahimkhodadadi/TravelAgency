using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Common.DataProcessing.Abstractions;

public interface ISortBy
{
}

public interface ISortBy<TEntity> : ISortBy
    where TEntity : class, IEntity
{
    abstract IQueryable<TEntity> Apply(IQueryable<TEntity> queryable);
}