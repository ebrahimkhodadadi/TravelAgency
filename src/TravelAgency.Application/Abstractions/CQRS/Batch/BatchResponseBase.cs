using TravelAgency.Application.Features;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Application.Abstractions.CQRS.Batch;

public abstract record BatchResponseBase<TResponseKey> : IBatchResponse
    where TResponseKey : struct, IUniqueKey
{
    protected BatchResponseBase(IList<BatchResponseEntry> entries)
    {
        Entries = entries;
    }

    public IList<BatchResponseEntry> Entries { get; set; }
}