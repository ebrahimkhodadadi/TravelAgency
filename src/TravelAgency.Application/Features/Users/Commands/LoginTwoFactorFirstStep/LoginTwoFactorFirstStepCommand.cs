using TravelAgency.Application.Abstractions.CQRS;

namespace TravelAgency.Application.Features.Users.Commands.LoginTwoFactorFirstStep;

public sealed record LoginTwoFactorFirstStepCommand
(
    string Email,
    string Password
)
    : ICommand;