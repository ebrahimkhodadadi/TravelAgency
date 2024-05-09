using MediatR;
using TravelAgency.Domain.Common.Errors;
using System.Collections.Frozen;
using System.Linq.Expressions;
using System.Reflection;
using TravelAgency.Application.Abstractions;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;
using TravelAgency.Domain.Common.Results.Abstractions;
using TravelAgency.Domain.Common.Utilities;
using TravelAgency.Persistence.Framework;
using TravelAgency.Persistence.Pipelines;
using ZiggyCreatures.Caching.Fusion;
using static TravelAgency.Domain.Common.Utilities.ReflectionUtilities;

namespace TravelAgency.Persistence.Cache;

public static partial class PersistenceCache
{
    /// <summary>
    /// This cache stores key-value pairs where: EntityId type is the key and a value is a tuple of corresponding Entity type and generic method that requires these two types.
    /// This cache is provided due to the performance optimizations. We do not want to use reflection calls for each request.
    /// </summary>
    /// <example>Key: typeof(ProductId), Value: CheckCacheAndDatabase<Product, ProductId> method</example>
    public static readonly FrozenDictionary<Type, Func<TravelAgencyDbContext, IFusionCache, IEntityId, CancellationToken, Task<Error>>> ValidationCache;

    private static FrozenDictionary<Type, Func<TravelAgencyDbContext, IFusionCache, IEntityId, CancellationToken, Task<Error>>> CreateValidationCache()
    {
        Dictionary<Type, Func<TravelAgencyDbContext, IFusionCache, IEntityId, CancellationToken, Task<Error>>> validationCache = [];
        var entityIdTypes = GetEntityIdTypes();

        foreach (var entityIdType in entityIdTypes)
        {
            Type entityType = GetEntityTypeFromEntityIdType(entityIdType);

            MethodInfo checkCacheAndDatabasedMethod = typeof(ReferenceValidationPipeline<IRequest<IResult<IResponse>>, IResult<IResponse>>)
                .GetSingleGenericMethod
                (
                    nameof(ReferenceValidationPipeline<IRequest<IResult<IResponse>>, IResult<IResponse>>.CheckCacheAndDatabase),
                    entityType,
                    entityIdType
                );

            var compiledFunc = CompileFunc(entityIdType, checkCacheAndDatabasedMethod);

            validationCache.Add(entityIdType, compiledFunc);
        }

        return validationCache.ToFrozenDictionary();
    }

    /// <summary>
    /// This method compiles the method info to func, so the performance will be increased. 
    /// However, if the level of complicity is too hight, we can store methodInfo in the cache and then compile the method at runtime. 
    /// If so, we can use non static version of CheckCacheAndDatabase method (without context and cache variable explicitly passed as a parameters)
    /// </summary>
    /// <param name="entityIdType">dynamically obtained entityIdType</param>
    /// <param name="methodInfo">methodInfo to compile</param>
    /// <returns></returns>
    private static Func<TravelAgencyDbContext, IFusionCache, IEntityId, CancellationToken, Task<Error>> CompileFunc(Type entityIdType, MethodInfo methodInfo)
    {
        var param1 = Expression.Parameter(typeof(TravelAgencyDbContext));
        var param2 = Expression.Parameter(typeof(IFusionCache));
        var param3 = Expression.Parameter(typeof(IEntityId));
        var param4 = Expression.Parameter(typeof(CancellationToken));

        var correctParameters = new Expression[]
        {
            param1,
            param2,
            Expression.Convert(param3, entityIdType), //we convert the incorrect type IEntityId to correct EntityId type
            param4
        };

        var lambda = Expression.Lambda<Func<TravelAgencyDbContext, IFusionCache, IEntityId, CancellationToken, Task<Error>>>
        (
            Expression.Call(null, methodInfo, correctParameters),
            false,
            param1,
            param2,
            param3,
            param4
        );

        return lambda.Compile();
    }
}