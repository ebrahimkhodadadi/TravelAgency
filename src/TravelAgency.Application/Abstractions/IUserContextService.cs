using System.Security.Claims;
using TravelAgency.Domain.Users;

namespace TravelAgency.Application.Abstractions;

public interface IUserContextService
{
    ClaimsPrincipal? User { get; }
    UserId? UserId { get; }
    CustomerId? CustomerId { get; }
    string? Username { get; }
}
