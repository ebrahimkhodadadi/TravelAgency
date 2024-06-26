﻿using TravelAgency.Domain.Common.Errors;
using TravelAgency.Application.Abstractions;
using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Common.Results.Abstractions;
using TravelAgency.Domain.Users;
using TravelAgency.Domain.Users.Enumerations;

namespace TravelAgency.Application.Features.Users.Commands.AddPermissionToRole;

internal sealed class AddPermissionToRoleCommandHandler(IUserRepository userRepository, IValidator validator)
    : ICommandHandler<AddPermissionToRoleCommand>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IValidator _validator = validator;

    public async Task<IResult> Handle(AddPermissionToRoleCommand command, CancellationToken cancellationToken)
    {
        var role = Role.FromName(command.Role);
        var permission = Permission.FromName(command.Permission);

        _validator
            .If(role is null, Error.NotFound(nameof(Role), command.Role, "Roles are case sensitive."))
            .If(permission is null, Error.NotFound(nameof(Permission), command.Permission, "Permissions are case sensitive."))
            .If(role == Role.Administrator, Error.InvalidOperation("Adding permission to Administrator role is forbidden."));

        if (_validator.IsInvalid)
        {
            return _validator.Failure();
        }

        var roleWithPermissions = await _userRepository
            .GetRolePermissionsAsync(role!, cancellationToken);

        _validator
            .If(roleWithPermissions!.Permissions.Any(x => x.Name == permission!.Name), Error.AlreadyExists(nameof(Permission), command.Permission));

        if (_validator.IsInvalid)
        {
            return _validator.Failure();
        }

        roleWithPermissions!
            .Permissions
            .Add(permission!);

        return Result.Success();
    }
}