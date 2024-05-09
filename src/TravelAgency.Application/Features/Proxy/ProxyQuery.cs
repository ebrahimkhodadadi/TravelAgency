using TravelAgency.Domain.Common.DataProcessing.Proxy;

namespace TravelAgency.Application.Features.Proxy;

public record ProxyQuery
(
    string Entity,
    OffsetOrCursorPage Page,
    DynamicFilter? Filter,
    DynamicSortBy? SortBy,
    DynamicMapping? Mapping
);
