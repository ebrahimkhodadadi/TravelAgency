using Microsoft.AspNetCore.Authorization;
using TravelAgency.Domain.Enums;
using TravelAgency.Domain.Common.Enums;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Users;
using TravelAgency.Presentation.Authentication.RolePermissionAuthentication;

namespace TravelAgency.Tests.Integration.Container.Api;

public sealed class TestUserAuthorizationService : IUserAuthorizationService
{
    private static readonly Ulid _testUserUlid = Ulid.Parse("01AN4Z07BY79KA1307SR9X4MV3");

    public Result<UserId> GetUserId(AuthorizationHandlerContext context)
    {
        return UserId.Create(_testUserUlid);
    }

    public Task<bool> HasPermissionsAsync(UserId userId, LogicalOperation logicalOperation, params Permission[] permissions)
    {
        return Task.FromResult(true);
    }

    public Task<bool> HasRolesAsync(UserId userId, params Role[] roles)
    {
        return Task.FromResult(true);
    }
}