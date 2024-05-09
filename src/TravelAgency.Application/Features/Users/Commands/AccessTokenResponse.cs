using TravelAgency.Application.Abstractions;
using static TravelAgency.Application.Features.Users.Commands.AccessTokenResponse;

namespace TravelAgency.Application.Features.Users.Commands;

public sealed record AccessTokenResponse(string AccessToken, int ExpiresInMinutes, string RefreshToken, string TokenType = Bearer) : IResponse
{
    public const string Bearer = nameof(Bearer);
}