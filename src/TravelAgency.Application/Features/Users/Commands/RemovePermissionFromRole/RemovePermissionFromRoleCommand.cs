using TravelAgency.Application.Abstractions.CQRS;

namespace TravelAgency.Application.Features.Users.Commands.RemovePermissionFromRole;

public sealed record RemovePermissionFromRoleCommand
(
    string Role,
    string Permission
)
    : ICommand;