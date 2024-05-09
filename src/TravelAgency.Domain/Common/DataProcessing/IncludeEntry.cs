using System.Linq.Expressions;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Common.DataProcessing;

public sealed record IncludeEntry<TEntity>(LambdaExpression Property, Type EntityType, Type PropertyType, Type? PreviousPropertyType, IncludeType IncludeType)
    where TEntity : class, IEntity;

public enum IncludeType
{
    Include = 1,
    ThenInclude = 2
}
