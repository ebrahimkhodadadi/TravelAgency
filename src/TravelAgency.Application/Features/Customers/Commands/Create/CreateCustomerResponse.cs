using TravelAgency.Application.Abstractions;

namespace TravelAgency.Application.Features.Customers.Commands.Create;

public sealed record CreateCustomerResponse
(
    Ulid Id
) : IResponse;