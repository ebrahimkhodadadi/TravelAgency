using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Application.Features.Users.Commands;

namespace TravelAgency.Application.Features.Users.Commands.LogUser;

public sealed record LogUserCommand
(
    string Email,
    string Password
)
    : ICommand<AccessTokenResponse>;