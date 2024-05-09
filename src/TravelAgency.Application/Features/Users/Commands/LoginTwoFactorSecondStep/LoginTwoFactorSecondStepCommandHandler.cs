using Microsoft.AspNetCore.Identity;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Application.Abstractions;
using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Application.Features.Users.Commands;
using TravelAgency.Application.Utilities;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Common.Results.Abstractions;
using TravelAgency.Domain.Users;
using TravelAgency.Domain.Users.ValueObjects;

namespace TravelAgency.Application.Features.Users.Commands.LoginTwoFactorSecondStep;

internal sealed class LoginTwoFactorSecondStepCommandHandler
(
    IUserRepository userRepository,
    ISecurityTokenService securityTokenService,
    IValidator validator,
    IPasswordHasher<User> passwordHasher
)
    : ICommandHandler<LoginTwoFactorSecondStepCommand, AccessTokenResponse>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ISecurityTokenService _securityTokenService = securityTokenService;
    private readonly IValidator _validator = validator;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

    public async Task<IResult<AccessTokenResponse>> Handle(LoginTwoFactorSecondStepCommand command, CancellationToken cancellationToken)
    {
        ValidationResult<Email> emailResult = Email.Create(command.Email);
        ValidationResult<Password> passwordResult = Password.Create(command.Password);
        ValidationResult<TwoFactorTokenHash> twoFactorTokenResult = TwoFactorTokenHash.Create(command.TwoFactorToken);

        _validator
            .Validate(emailResult)
            .Validate(passwordResult)
            .Validate(twoFactorTokenResult);

        if (_validator.IsInvalid)
        {
            return _validator.Failure<AccessTokenResponse>();
        }

        User? user = await _userRepository
            .GetByEmailAsync(emailResult.Value, cancellationToken);

        _validator
            .If(user?.TwoFactorTokenHash is null, thenError: Error.InvalidArgument("User not found or token is null"));

        if (_validator.IsInvalid)
        {
            return _validator.Failure<AccessTokenResponse>();
        }

        var passwordVerificationResult = _passwordHasher
            .VerifyHashedPassword(user!, user!.PasswordHash.Value, passwordResult.Value.Value);

        var twoFactorTokenVerificaitonResult = _passwordHasher
            .VerifyHashedPassword(user!, user!.TwoFactorTokenHash!.Value, command.TwoFactorToken);

        _validator
            .If(passwordVerificationResult is PasswordVerificationResult.Failed, thenError: Error.InvalidArgument("Invalid Password"))
            .If(twoFactorTokenVerificaitonResult is PasswordVerificationResult.Failed, thenError: Error.InvalidArgument("Invalid TwoFactorToken"))
            .If(_securityTokenService.HasTwoFactorTokenExpired(user!.TwoFactorTokenCreatedOn), thenError: Error.InvalidArgument("TwoFactorToken has expired"));

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
        user!.ClearTwoFactorToken();

        return accessTokenResult
            .ToResult();
    }
}
