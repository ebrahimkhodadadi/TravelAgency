using System.Linq.Expressions;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;
using TravelAgency.Domain.Common.DataProcessing;
using TravelAgency.Domain.Common.DataProcessing.Abstractions;
using TravelAgency.Persistence.Utilities;

namespace TravelAgency.Persistence.Specifications;

public sealed class LikeProvider<TEntity> : ILikeProvider<TEntity>
    where TEntity : class, IEntity
{
    public IQueryable<TEntity> Apply(IQueryable<TEntity> queryable, IList<LikeEntry<TEntity>> likeEntries)
    {
        return queryable.Like(likeEntries);
    }

    public Expression CreateLikeExpression(ParameterExpression parameter, Expression property, string likeTerm)
    {
        return QueryableUtilities.CreateLikeExpression(parameter, property, likeTerm);
    }

    public Expression CreateLikeExpression(ParameterExpression parameter, string property, string likeTerm)
    {
        return QueryableUtilities.CreateLikeExpression(parameter, property, likeTerm);
    }
}