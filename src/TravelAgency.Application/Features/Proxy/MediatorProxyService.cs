using TravelAgency.Domain.Common.Errors;
using System.Collections.Frozen;
using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Domain.Common.DataProcessing.Proxy;
using TravelAgency.Domain.Common.Discriminators;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Application.Abstractions;

namespace TravelAgency.Application.Features.Proxy;

public partial class MediatorProxyService(IValidator validator) : IMediatorProxyService
{
    private static readonly FrozenDictionary<QueryDiscriminator, Func<ProxyQuery, IQuery<PageResponse<DataTransferObjectResponse>>>> _strategyCache =
        StrategyCacheFactory<QueryDiscriminator, Func<ProxyQuery, IQuery<PageResponse<DataTransferObjectResponse>>>>
            .CreateFor<MediatorProxyService, QueryStrategyAttribute>();

    private readonly IValidator _validator = validator;

    public Result<IQuery<PageResponse<DataTransferObjectResponse>>> Map(ProxyQuery proxyQuery)
    {
        _validator
            .If(PageIsNotOffsetOrCursorPage(proxyQuery.Page), Error.InvalidArgument("Cursor or PageNumber must be provided."))
            .If(PageIsBothOffsetAndCursorPage(proxyQuery.Page), Error.InvalidArgument("Both Cursor and PageNumber cannot be provided."));

        if (_validator.IsInvalid)
        {
            return Failure();
        }

        var strategyKey = new QueryDiscriminator(proxyQuery.Entity, proxyQuery.Page.GetPageType());

        _validator
            .If(_strategyCache.TryGetValue(strategyKey, out var @delegate) is false, Error.InvalidOperation($"Entity '{proxyQuery.Entity}' with page type '{proxyQuery.Page.GetPageType().Name}' is not supported. Supported strategies: [{string.Join(", ", _strategyCache.Keys.Select(x => (x.Entity, x.PageType.Name)))}]"));

        if (_validator.IsInvalid)
        {
            return Failure();
        }

        return Result.Success(@delegate!(proxyQuery));
    }

    private static bool PageIsNotOffsetOrCursorPage(OffsetOrCursorPage offsetOrCursorPage)
    {
        return offsetOrCursorPage.Cursor is null && offsetOrCursorPage.PageNumber is null;
    }

    private static bool PageIsBothOffsetAndCursorPage(OffsetOrCursorPage offsetOrCursorPage)
    {
        return offsetOrCursorPage.Cursor is not null && offsetOrCursorPage.PageNumber is not null;
    }

    private ValidationResult<IQuery<PageResponse<DataTransferObjectResponse>>> Failure()
    {
        return ValidationResult<IQuery<PageResponse<DataTransferObjectResponse>>>
            .WithErrors(_validator.Failure().ValidationErrors);
    }
}