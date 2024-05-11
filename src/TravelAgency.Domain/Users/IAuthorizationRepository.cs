using TravelAgency.Domain.Enums;
using TravelAgency.Domain.Common.Enums;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Users;

public interface IAuthorizationRepository : IRepository
{
    Task<HashSet<string>> GetPermissionsAsync(UserId userId);
    Task<bool> HasPermissionsAsync(UserId userId, Permission[] requiredPermissions, LogicalOperation logicalOperation = LogicalOperation.And);
    Task<bool> HasRolesAsync(UserId userId, Role[] roles);
}