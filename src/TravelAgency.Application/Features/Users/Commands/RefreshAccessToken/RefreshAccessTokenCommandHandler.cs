﻿using TravelAgency.Domain.Common.Errors;
using TravelAgency.Application.Abstractions;
using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Application.Features.Users.Commands;
using TravelAgency.Application.Utilities;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Common.Results.Abstractions;
using TravelAgency.Domain.Users;
using TravelAgency.Domain.Users.ValueObjects;
using static TravelAgency.Domain.Users.Errors.DomainErrors.PasswordOrEmailError;

namespace TravelAgency.Application.Features.Users.Commands.RefreshAccessToken;

internal sealed class RefreshAccessTokenCommandHandler
(
    IUserRepository userRepository,
    ISecurityTokenService securityTokenService,
    IValidator validator
)
    : ICommandHandler<RefreshAccessTokenCommand, AccessTokenResponse>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ISecurityTokenService _securityTokenService = securityTokenService;
    private readonly IValidator _validator = validator;

    public async Task<IResult<AccessTokenResponse>> Handle(RefreshAccessTokenCommand command, CancellationToken cancellationToken)
    {
        var emailClaim = _securityTokenService.GetClaimFromToken(command.AccessToken, nameof(Email));
        ValidationResult<RefreshToken> refreshToken = RefreshToken.Create(command.RefreshToken);
        var hasRefreshTokenExpired = _securityTokenService.HasRefreshTokenExpired(command.AccessToken);

        _validator
            .Validate(refreshToken)
            .If(emailClaim.IsFailure, emailClaim.Error)
            .If(hasRefreshTokenExpired.IsFailure, hasRefreshTokenExpired.Error);

        if (_validator.IsInvalid)
        {
            return _validator.Failure<AccessTokenResponse>();
        }

        var email = Email.Create(emailClaim.Value!.Value!);

        _validator
            .Validate(email)
            .If(emailClaim.IsFailure, emailClaim.Error)
            .If(hasRefreshTokenExpired.Value, Error.InvalidArgument("Refresh Token has expired"));

        if (_validator.IsInvalid)
        {
            return _validator.Failure<AccessTokenResponse>();
        }

        User? user = await _userRepository
            .GetByEmailAsync(email.Value, cancellationToken);

        _validator
            .If(user is null, thenError: InvalidPasswordOrEmail);

        if (_validator.IsInvalid)
        {
            return _validator.Failure<AccessTokenResponse>();
        }

        var accessTokenResult = _securityTokenService.GenerateJwt(user!);
        var refreshTokenResult = RefreshToken.Create(accessTokenResult.RefreshToken);

        _validator
            .Validate(refreshTokenResult);

        if (_validator.IsInvalid)
        {
            return _validator.Failure<AccessTokenResponse>();
        }

        user!.RefreshToken = refreshTokenResult.Value;

        return accessTokenResult
            .ToResult();
    }
}
