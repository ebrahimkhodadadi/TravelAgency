using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Common.Errors;
using System.Linq.Dynamic.Core;
using TravelAgency.Application.Utilities;
using TravelAgency.Domain.Common.BaseTypes;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Results.Abstractions;
using TravelAgency.Persistence.Framework;
using TravelAgency.Persistence.Utilities;
using ZiggyCreatures.Caching.Fusion;
using static TravelAgency.Domain.Common.Utilities.ReflectionUtilities;
using static TravelAgency.Persistence.Cache.PersistenceCache;
using static TravelAgency.Persistence.Utilities.QueryableUtilities;
using TravelAgency.Domain.Common.Utilities;

namespace TravelAgency.Persistence.Pipelines;

public sealed class ReferenceValidationPipeline<TRequest, TResponse>(TravelAgencyDbContext context, IFusionCache fusionCache)
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class, IResult
{
    private readonly TravelAgencyDbContext _context = context;
    private readonly IFusionCache _fusionCache = fusionCache;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var entityIds = typeof(TRequest)
            .GetProperties()
            .Where(property => property.Implements<IEntityId>())
            .Select(entityId => entityId.GetValue(request) as IEntityId);

        Error[] errors = entityIds
            .Select(entityId => Validate(entityId!, cancellationToken))
            .Select(task => task.Result)
            .Where(error => error != Error.None)
            .Distinct()
            .ToArray();

        if (errors.Length is not 0)
        {
            return errors.CreateValidationResult<TResponse>();
        }

        return await next();
    }

    private async Task<Error> Validate(IEntityId entityId, CancellationToken cancellationToken)
    {
        return await ValidationCache[entityId.GetType()](_context, _fusionCache, entityId, cancellationToken);
    }

    public static async Task<Error> CheckCacheAndDatabase<TEntity, TEntityId>
    (
        TravelAgencyDbContext context,
        IFusionCache cache,
        TEntityId entityId,
        CancellationToken cancellationToken
    )
        where TEntity : Entity<TEntityId>
        where TEntityId : struct, IEntityId<TEntityId>
    {
        var cacheReferenceCheckKey = entityId.ToCacheReferenceCheckKey();

        var isEntityInCache = await cache.AnyAsync<TEntity, TEntityId>(cacheReferenceCheckKey, cancellationToken);

        if (isEntityInCache)
        {
            return Error.None;
        }

        var isEntityInDatabase = await context
            .Set<TEntity>()
            .AnyAsync(entityId, cancellationToken);

        if (isEntityInDatabase)
        {
            //We should not store entities in the cache using this pipeline, therefore we just store null
            await cache.SetAsync(cacheReferenceCheckKey, default(TEntity), token: cancellationToken);
            return Error.None;
        }

        return Error.InvalidReference(entityId.Value, typeof(TEntity).Name);
    }
}
