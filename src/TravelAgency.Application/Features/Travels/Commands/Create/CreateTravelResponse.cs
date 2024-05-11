namespace TravelAgency.Application.Features.Bills.Commands.Create;

public sealed record CreateTravelResponse
(
    Ulid Id
) : IResponse;