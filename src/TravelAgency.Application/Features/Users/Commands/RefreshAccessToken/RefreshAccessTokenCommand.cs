using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Application.Features.Users.Commands;

namespace TravelAgency.Application.Features.Users.Commands.RefreshAccessToken;

public sealed record RefreshAccessTokenCommand
(
    string AccessToken,
    string RefreshToken
)
    : ICommand<AccessTokenResponse>;