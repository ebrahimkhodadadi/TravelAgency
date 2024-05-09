using TravelAgency.Application.Abstractions;

namespace TravelAgency.Application.Features.Users.Queries.GetUserRoles;

public sealed record RolesResponse(List<string> Roles) : IResponse;