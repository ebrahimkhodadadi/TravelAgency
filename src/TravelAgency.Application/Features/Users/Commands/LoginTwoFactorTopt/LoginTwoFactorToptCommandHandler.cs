﻿using Microsoft.AspNetCore.Identity;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Application.Abstractions;
using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Application.Utilities;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Common.Results.Abstractions;
using TravelAgency.Domain.Users;
using TravelAgency.Domain.Users.ValueObjects;
using static TravelAgency.Domain.Users.Errors.DomainErrors.PasswordOrEmailError;

namespace TravelAgency.Application.Features.Users.Commands.LoginTwoFactorTopt;

internal sealed class LoginTwoFactorToptCommandHandler
(
    IUserRepository userRepository,
    IValidator validator,
    ISecurityTokenService securityTokenService,
    IPasswordHasher<User> passwordHasher,
    IToptService toptService
)
    : ICommandHandler<LoginTwoFactorToptCommand, AccessTokenResponse>
{
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly IToptService _toptService = toptService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IValidator _validator = validator;
    private readonly ISecurityTokenService _securityTokenService = securityTokenService;

    public async Task<IResult<AccessTokenResponse>> Handle(LoginTwoFactorToptCommand command, CancellationToken cancellationToken)
    {
        ValidationResult<Email> emailResult = Email.Create(command.Email);
        ValidationResult<Password> passwordResult = Password.Create(command.Password);

        _validator
            .Validate(emailResult)
            .Validate(passwordResult);

        if (_validator.IsInvalid)
        {
            return _validator.Failure<AccessTokenResponse>();
        }

        User? user = await _userRepository
            .GetByEmailAsync(emailResult.Value, cancellationToken);

        _validator
            .If(user is null, thenError: InvalidPasswordOrEmail);

        if (_validator.IsInvalid)
        {
            return _validator.Failure<AccessTokenResponse>();
        }

        var result = _passwordHasher
            .VerifyHashedPassword(user!, user!.PasswordHash.Value, passwordResult.Value.Value);

        _validator
            .If(result is PasswordVerificationResult.Failed, thenError: InvalidPasswordOrEmail)
            .If(user.TwoFactorToptSecret is null, Error.New(nameof(user.TwoFactorToptSecret), $"{nameof(user.TwoFactorToptSecret)} is not configured"));

        if (_validator.IsInvalid)
        {
            return _validator.Failure<AccessTokenResponse>();
        }

        _validator
            .If(_toptService.VerifyCode(user.TwoFactorToptSecret!.Value, command.Code) is false, thenError: Error.InvalidArgument($"{nameof(command.Code)} is invalid"));

        if (_validator.IsInvalid)
        {
            return _validator.Failure<AccessTokenResponse>();
        }

        var accessTokenResponse = _securityTokenService.GenerateJwt(user);

        var refreshTokenResult = RefreshToken.Create(accessTokenResponse.RefreshToken);

        _validator
            .Validate(refreshTokenResult);

        if (_validator.IsInvalid)
        {
            return _validator.Failure<AccessTokenResponse>();
        }

        user.RefreshToken = refreshTokenResult.Value;

        return accessTokenResponse
            .ToResult();
    }
}
