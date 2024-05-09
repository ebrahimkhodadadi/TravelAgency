using System.Security.Claims;
using TravelAgency.Application.Features.Users.Commands;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Users;

namespace TravelAgency.Application.Abstractions;

public interface ISecurityTokenService
{
    AccessTokenResponse GenerateJwt(User user);
    Result<Claim?> GetClaimFromToken(string token, string claimInvariantName);
    Result<bool> HasRefreshTokenExpired(string token);
    bool HasTwoFactorTokenExpired(DateTimeOffset? twoFactorTokenCreatedOn);
}