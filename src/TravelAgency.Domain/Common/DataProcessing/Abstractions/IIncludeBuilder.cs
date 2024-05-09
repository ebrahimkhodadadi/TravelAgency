using System.Linq.Expressions;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Common.DataProcessing.Abstractions;

public interface IIncludeBuilder<TEntity>
{
    IThenIncludeBuilder<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> includeExpression)
        where TProperty : class, IEntity;
    IThenIncludeBuilder<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, IEnumerable<TProperty>>> includeExpression)
        where TProperty : class, IEntity;
}