using TravelAgency.Application.Abstractions;

namespace TravelAgency.Application.Features.Users.Queries.GetRolePermissions;

public sealed record RolePermissionsResponse(List<string> Permissions) : IResponse;