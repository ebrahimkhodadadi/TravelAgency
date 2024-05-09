using FluentValidation;

namespace TravelAgency.Application.Features.Users.Commands.AddPermissionToRole;

internal sealed class AddPermissionToRoleCommandValidator : AbstractValidator<AddPermissionToRoleCommand>
{
    public AddPermissionToRoleCommandValidator()
    {
        RuleFor(x => x.Role).NotEmpty();
        RuleFor(x => x.Permission).NotEmpty();
    }
}