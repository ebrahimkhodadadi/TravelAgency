using System.Security.Claims;
using TravelAgency.Application.Abstractions;
using TravelAgency.Domain.Users;
using static TravelAgency.Tests.Integration.Constants.Constants;

namespace TravelAgency.Tests.Integration.Persistence;

/// <summary>
/// Test context service, used to set the "CreatedBy" field to the user name
/// </summary>
public sealed class TestContextService : IUserContextService
{
    public ClaimsPrincipal? User => null;

    public UserId? UserId => null;

    public string? Username => TestUser.Username;

    public CustomerId? CustomerId => null;
}