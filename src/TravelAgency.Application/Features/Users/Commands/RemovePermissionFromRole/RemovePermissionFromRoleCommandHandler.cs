using TravelAgency.Domain.Common.Errors;
using TravelAgency.Application.Abstractions;
using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Common.Results.Abstractions;
using TravelAgency.Domain.Users;
using TravelAgency.Domain.Users.Enumerations;

namespace TravelAgency.Application.Features.Users.Commands.RemovePermissionFromRole;

internal sealed class RemovePermissionFromRoleCommandHandler(IUserRepository userRepository, IValidator validator)
    : ICommandHandler<RemovePermissionFromRoleCommand>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IValidator _validator = validator;

    public async Task<IResult> Handle(RemovePermissionFromRoleCommand command, CancellationToken cancellationToken)
    {
        var role = Role.FromName(command.Role);
        var permission = Permission.FromName(command.Permission);

        _validator
            .If(role is null, Error.NotFound(nameof(Role), command.Role, "Roles are case sensitive."))
            .If(permission is null, Error.NotFound(nameof(Permission), command.Permission, "Permissions are case sensitive."))
            .If(role == Role.Administrator, Error.InvalidOperation("Removing permission from to Administrator role is forbidden."));

        if (_validator.IsInvalid)
        {
            return _validator.Failure();
        }

        var roleWithPermissions = await _userRepository
            .GetRolePermissionsAsync(role!, cancellationToken);

        _validator
            .If(roleWithPermissions!.Permissions.Any(x => x.Name == permission!.Name) is false, Error.InvalidOperation("Role does not contain given permission."));

        if (_validator.IsInvalid)
        {
            return _validator.Failure();
        }

        var permissionToRemove = roleWithPermissions
            .Permissions
            .First(x => x.Name == permission!.Name);

        roleWithPermissions!
            .Permissions
            .Remove(permissionToRemove);

        return Result.Success();
    }
}