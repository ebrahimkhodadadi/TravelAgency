namespace TravelAgency.Application.Features.Bills.Commands.Create;

public sealed record CreateBillResponse
(
    Ulid Id
) : IResponse;