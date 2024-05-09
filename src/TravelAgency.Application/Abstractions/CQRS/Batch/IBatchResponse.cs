using TravelAgency.Application.Abstractions;
using TravelAgency.Application.Features;

namespace TravelAgency.Application.Abstractions.CQRS.Batch;

public interface IBatchResponse : IResponse
{
    IList<BatchResponseEntry> Entries { get; set; }
}