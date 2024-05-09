using Microsoft.AspNetCore.Authorization;
using TravelAgency.Domain.Common.Enums;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Enums;
using TravelAgency.Domain.Users;

namespace TravelAgency.Presentation.Authentication.RolePermissionAuthentication;

public interface IUserAuthorizationService
{
    Result<UserId> GetUserId(AuthorizationHandlerContext context);
    Task<bool> HasPermissionsAsync(UserId userId, LogicalOperation logicalOperation, params Permission[] permissions);
    Task<bool> HasRolesAsync(UserId userId, params Role[] roles);
}