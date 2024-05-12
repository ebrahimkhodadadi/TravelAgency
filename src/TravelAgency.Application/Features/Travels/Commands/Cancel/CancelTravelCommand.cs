using TravelAgency.Application.Abstractions.CQRS;

namespace TravelAgency.Application.Features.Travels.Commands.Cancel
{
    public sealed record CancelTravelCommand(
        Ulid travelId
        ) : ICommand;
}
