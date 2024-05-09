using System.Linq.Expressions;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Common.DataProcessing.Abstractions;

public interface ILikeProvider<TEntity>
    where TEntity : class, IEntity
{
    IQueryable<TEntity> Apply(IQueryable<TEntity> queryable, IList<LikeEntry<TEntity>> likeEntries);
    Expression CreateLikeExpression(ParameterExpression parameter, Expression property, string likeTerm);
    Expression CreateLikeExpression(ParameterExpression parameter, string property, string likeTerm);
}