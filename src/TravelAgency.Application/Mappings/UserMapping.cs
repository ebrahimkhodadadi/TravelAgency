using TravelAgency.Application.Features.Users.Commands.RegisterUser;
using TravelAgency.Application.Features.Users.Queries.GetRolePermissions;
using TravelAgency.Application.Features.Users.Queries.GetUserByUsername;
using TravelAgency.Application.Features.Users.Queries.GetUserRoles;
using TravelAgency.Domain.Users;
using TravelAgency.Domain.Users.Enumerations;

namespace TravelAgency.Application.Mappings;

public static class UserMapping
{
    public static RegisterUserResponse ToCreateResponse(this User userToCreate)
    {
        return new RegisterUserResponse(userToCreate.Id.Value);
    }

    public static UserResponse ToResponse(this User user)
    {
        return new UserResponse
        (
            user.Id.Value,
            user.Username.Value,
            user.Email.Value,
            user.CustomerId?.Value
        );
    }

    public static RolesResponse ToResponse(this IReadOnlyCollection<Role> roles)
    {
        return new RolesResponse(roles.Select(role => role.Name).ToList());
    }

    public static RolePermissionsResponse ToResponse(this Role role)
    {
        return new RolePermissionsResponse(role.Permissions.Select(x => x.Name).ToList());
    }
}