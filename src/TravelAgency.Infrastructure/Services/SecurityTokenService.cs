using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TravelAgency.Domain.Common.Errors;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelAgency.Application.Abstractions;
using TravelAgency.Application.Features.Users.Commands;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Common.Utilities;
using TravelAgency.Domain.Users;
using TravelAgency.Domain.Users.ValueObjects;
using TravelAgency.Infrastructure.Options;
using TravelAgency.Infrastructure.Policies;
using static System.StringComparison;

namespace TravelAgency.Infrastructure.Services;

internal sealed class SecurityTokenService(IOptions<AuthenticationOptions> options, TimeProvider timeProvider) : ISecurityTokenService
{
    private readonly AuthenticationOptions _options = options.Value;
    private readonly TimeProvider _timeProvider = timeProvider;

    public AccessTokenResponse GenerateJwt(User user)
    {
        var claims = new Claim[]
        {
            new(ClaimTypes.NameIdentifier, user.Id.Value.ToString()),
            new(JwtRegisteredClaimNames.Name, user.Username.Value),
            new(JwtRegisteredClaimNames.Email, user.Email.Value),
            new(ClaimPolicies.CustomerId, user switch
            {
                { CustomerId: not null } => $"{user.CustomerId}",
                _ => string.Empty
            })
        };

        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var expires = _timeProvider.GetLocalNow().AddMinutes(_options.AccessTokenExpirationInMinutes);

        var token = new JwtSecurityToken
        (
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: expires.DateTime,
            signingCredentials: signingCredentials,
            notBefore: _timeProvider.GetLocalNow().DateTime
        );

        var accessToken = new JwtSecurityTokenHandler()
            .WriteToken(token);

        var refreshToken = RandomUtilities.GenerateString(RefreshToken.Length);

        return new AccessTokenResponse(accessToken, _options.AccessTokenExpirationInMinutes, refreshToken);
    }

    public Result<Claim?> GetClaimFromToken(string token, string claimInvariantName)
    {
        SecurityToken securityToken = GetSecurityToken(token);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, InvariantCultureIgnoreCase))
        {
            return Result.Failure<Claim?>(Error.InvalidArgument("Invalid token"));
        }

        return jwtSecurityToken.Claims.FirstOrDefault(x => x.Type.Equals(claimInvariantName, InvariantCultureIgnoreCase));
    }

    public Result<bool> HasRefreshTokenExpired(string token)
    {
        SecurityToken securityToken = GetSecurityToken(token);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, InvariantCultureIgnoreCase))
        {
            return Result.Failure<bool>(Error.InvalidArgument("Invalid token"));
        }

        return securityToken.ValidFrom.AddDays(_options.RefreshTokenExpirationInDays) < _timeProvider.GetUtcNow();
    }

    public bool HasTwoFactorTokenExpired(DateTimeOffset? twoFactorTokenCreatedOn)
    {
        if (twoFactorTokenCreatedOn is null)
        {
            return true;
        }

        return ((DateTimeOffset)twoFactorTokenCreatedOn).AddSeconds(_options.TwoFactorTokenExpirationInSeconds) < _timeProvider.GetUtcNow();
    }

    private SecurityToken GetSecurityToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            ValidateLifetime = false,
            ValidAudiences = [_options.Audience],
            ValidIssuers = [_options.Issuer]
        };

        new JwtSecurityTokenHandler()
            .ValidateToken(token, tokenValidationParameters, out SecurityToken? securityToken);

        return securityToken;
    }
}