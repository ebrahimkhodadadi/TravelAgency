using System.Linq.Expressions;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Common.DataProcessing.Abstractions;

public interface IThenIncludeBuilder<TEntity, TProperty> : IIncludeBuilder<TEntity>
{
    IThenIncludeBuilder<TEntity, TNextProperty> ThenInclude<TNextProperty>(Expression<Func<TProperty, TNextProperty>> thenIncludeExpression)
        where TNextProperty : class, IEntity;
    IThenIncludeBuilder<TEntity, TNextProperty> ThenInclude<TNextProperty>(Expression<Func<TProperty, IEnumerable<TNextProperty>>> thenIncludeExpression)
        where TNextProperty : class, IEntity;
}
