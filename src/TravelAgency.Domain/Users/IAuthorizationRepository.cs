using TravelAgency.Domain.Enums;
using TravelAgency.Domain.Common.Enums;

namespace TravelAgency.Domain.Users;

public interface IAuthorizationRepository
{
    Task<HashSet<string>> GetPermissionsAsync(UserId userId);
    Task<bool> HasPermissionsAsync(UserId userId, Permission[] requiredPermissions, LogicalOperation logicalOperation = LogicalOperation.And);
    Task<bool> HasRolesAsync(UserId userId, Role[] roles);
}