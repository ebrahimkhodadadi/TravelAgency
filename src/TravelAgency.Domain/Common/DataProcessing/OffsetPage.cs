using TravelAgency.Domain.Common.DataProcessing.Abstractions;

namespace TravelAgency.Domain.Common.DataProcessing;

public sealed record OffsetPage : IOffsetPage
{
    public required int PageSize { get; init; }
    public required int PageNumber { get; init; }
}