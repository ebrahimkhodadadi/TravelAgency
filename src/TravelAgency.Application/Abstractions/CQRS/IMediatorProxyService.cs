using TravelAgency.Application.Features;
using TravelAgency.Application.Features.Proxy;
using TravelAgency.Domain.Common.Results;

namespace TravelAgency.Application.Abstractions.CQRS;

public interface IMediatorProxyService
{
    Result<IQuery<PageResponse<DataTransferObjectResponse>>> Map(ProxyQuery proxyQuery);
}