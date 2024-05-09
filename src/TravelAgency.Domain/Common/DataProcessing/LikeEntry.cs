using System.Linq.Expressions;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Common.DataProcessing;

public sealed record LikeEntry<TEntity>(Expression<Func<TEntity, string>> Property, string LikeTerm)
    where TEntity : class, IEntity;