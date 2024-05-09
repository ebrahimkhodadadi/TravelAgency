using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Application.Features.Users.Commands;

namespace TravelAgency.Application.Features.Users.Commands.LoginTwoFactorSecondStep;

public sealed record LoginTwoFactorSecondStepCommand
(
    string Email,
    string Password,
    string TwoFactorToken
)
    : ICommand<AccessTokenResponse>;