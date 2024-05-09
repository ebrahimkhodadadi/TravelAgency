using TravelAgency.Application.Abstractions.CQRS;

namespace TravelAgency.Application.Features.Users.Commands.AddPermissionToRole;

public sealed record AddPermissionToRoleCommand
(
    string Role,
    string Permission
)
    : ICommand;