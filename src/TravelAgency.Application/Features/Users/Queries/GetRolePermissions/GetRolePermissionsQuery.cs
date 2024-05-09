using TravelAgency.Application.Abstractions.CQRS;

namespace TravelAgency.Application.Features.Users.Queries.GetRolePermissions;

public sealed record GetRolePermissionsQuery(string Role) : IQuery<RolePermissionsResponse>;