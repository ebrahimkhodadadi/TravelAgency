using TravelAgency.Application.Abstractions;

namespace TravelAgency.Application.Features.Users.Commands.RegisterUser;

public sealed record RegisterUserResponse
(
    Ulid Id
) : IResponse;