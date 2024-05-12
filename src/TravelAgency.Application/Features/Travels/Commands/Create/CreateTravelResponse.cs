namespace TravelAgency.Application.Features.Travels.Commands.Create;

public sealed record CancelTravelResponse
(
    Ulid Id
) : IResponse;