using TravelAgency.Application.Abstractions.CQRS;

namespace TravelAgency.Application.Features.Users.Commands.RegisterUser;

public sealed record RegisterUserCommand
(
    string Username,
    string Email,
    string Password,
    string ConfirmPassword

)
    : ICommand<RegisterUserResponse>;