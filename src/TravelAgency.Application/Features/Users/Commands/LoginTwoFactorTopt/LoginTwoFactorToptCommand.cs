using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Application.Features.Users.Commands;

namespace TravelAgency.Application.Features.Users.Commands.LoginTwoFactorTopt;

public sealed record LoginTwoFactorToptCommand
(
    string Email,
    string Password,
    string Code
)
    : ICommand<AccessTokenResponse>;