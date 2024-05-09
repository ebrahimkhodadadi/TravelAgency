using TravelAgency.Application.Abstractions.CQRS;

namespace TravelAgency.Application.Features.Users.Queries.GetUserRoles;

public sealed record GetUserRolesByUsernameQuery(string Username) : IQuery<RolesResponse>;