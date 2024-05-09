using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Domain.Users;

namespace TravelAgency.Application.Features.Users.Commands.Revoke;

public sealed record RevokeRefreshTokenCommand(UserId UserId) : ICommand;