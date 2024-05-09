using TravelAgency.Domain.Common.DataProcessing.Abstractions;

namespace TravelAgency.Domain.Common.DataProcessing;

public sealed class CursorPage : ICursorPage
{
    public required int PageSize { get; init; }
    public required Ulid Cursor { get; init; }
}