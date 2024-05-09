using TravelAgency.Application.Abstractions;

namespace TravelAgency.Application.Features;

public record PageResponse<TValue> : IPageResponse
    where TValue : IResponse
{
    /// <summary>
    /// Generic list that stores the pagination result
    /// </summary>
    public IReadOnlyList<TValue> Items { get; private init; }

    protected PageResponse(IList<TValue> items)
    {
        Items = items.AsReadOnly();
    }
}