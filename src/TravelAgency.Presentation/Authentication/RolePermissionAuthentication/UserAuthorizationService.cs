using Microsoft.AspNetCore.Authorization;
using TravelAgency.Domain.Common.Enums;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Users;
using System.Security.Claims;

namespace TravelAgency.Presentation.Authentication.RolePermissionAuthentication;

public sealed class UserAuthorizationService(IAuthorizationRepository authorizationRepository) : IUserAuthorizationService
{
    private readonly IAuthorizationRepository _authorizationRepository = authorizationRepository;

    public Result<UserId> GetUserId(AuthorizationHandlerContext context)
    {
        string? userIdentifier = context
            .User
            .Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
            ?.Value;

        if (Ulid.TryParse(userIdentifier, out Ulid userUlid) is false)
        {
            return Result.Failure<UserId>(Error.ParseFailure<Ulid>(nameof(ClaimTypes.NameIdentifier)));
        }

        return UserId.Create(userUlid);
    }

    public async Task<bool> HasPermissionsAsync(UserId userId, LogicalOperation logicalOperation, params Domain.Enums.Permission[] permissions)
    {
        return await _authorizationRepository
            .HasPermissionsAsync(userId, permissions, logicalOperation);
    }

    public async Task<bool> HasRolesAsync(UserId userId, params Domain.Enums.Role[] roles)
    {
        return await _authorizationRepository
            .HasRolesAsync(userId, roles);
    }
}