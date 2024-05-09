using Microsoft.AspNetCore.Identity;
using TravelAgency.Application.Abstractions;
using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Application.Mappings;
using TravelAgency.Application.Utilities;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Common.Results.Abstractions;
using TravelAgency.Domain.Users;
using TravelAgency.Domain.Users.ValueObjects;
using static TravelAgency.Domain.Users.Errors.DomainErrors;

namespace TravelAgency.Application.Features.Users.Commands.RegisterUser;

internal sealed class RegisterUserCommandHandler(IUserRepository userRepository, IValidator validator, IPasswordHasher<User> passwordHasher)
    : ICommandHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly IValidator _validator = validator;

    public async Task<IResult<RegisterUserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        ValidationResult<Email> emailResult = Email.Create(request.Email);
        ValidationResult<Username> usernameResult = Username.Create(request.Username);
        ValidationResult<Password> passwordResult = Password.Create(request.Password);

        bool emailIsTaken = await _userRepository
            .IsEmailTakenAsync(emailResult.Value, cancellationToken);

        _validator
            .Validate(emailResult)
            .Validate(usernameResult)
            .Validate(passwordResult)
            .If(emailIsTaken, thenError: EmailError.EmailAlreadyTaken);

        if (_validator.IsInvalid)
        {
            return _validator.Failure<RegisterUserResponse>();
        }

        var result = RegisterUser(emailResult.Value, usernameResult.Value, passwordResult.Value);

        return result;
    }

    private IResult<RegisterUserResponse> RegisterUser(Email email, Username username, Password password)
    {
        var user = User.Create(UserId.New(), username, email);

        ValidationResult<PasswordHash> passwordHashResult = PasswordHash.Create(_passwordHasher.HashPassword(user, password.Value));

        _validator
            .Validate(passwordHashResult);

        if (_validator.IsInvalid)
        {
            return _validator.Failure<RegisterUserResponse>();
        }

        user.SetHashedPassword(passwordHashResult.Value);

        _userRepository.Add(user);

        return user
            .ToCreateResponse()
            .ToResult();
    }
}