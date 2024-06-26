﻿using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Enums;
using TravelAgency.Domain.Common.Enums;
using TravelAgency.Domain.Users;
using TravelAgency.Persistence.Framework;

namespace TravelAgency.Persistence.Repositories;

public sealed class AuthorizationRepository(TravelAgencyDbContext dbContext) : IAuthorizationRepository
{
    private readonly TravelAgencyDbContext _dbContext = dbContext;

    public async Task<HashSet<string>> GetPermissionsAsync(UserId userId)
    {
        var permissions = await _dbContext
            .Set<User>()
            .Include(x => x.Roles)
                .ThenInclude(x => x.Permissions)
            .Where(x => x.Id == userId)
            .SelectMany(x => x.Roles)
            .SelectMany(role => role.Permissions)
            .Select(permission => permission.Name)
            .ToArrayAsync();

        return permissions
            .ToHashSet();
    }

    public async Task<bool> HasPermissionsAsync(UserId userId, Permission[] requiredPermissions, LogicalOperation logicalOperation = LogicalOperation.And)
    {
        var distinctRequiredPermissions = requiredPermissions
            .Select(x => $"{x}")
            .Distinct()
            .ToArray();

        var userPermissionsQueryable = _dbContext
            .Set<User>()
            .Include(x => x.Roles)
                .ThenInclude(x => x.Permissions)
            .Where(x => x.Id == userId)
            .SelectMany(x => x.Roles)
            .SelectMany(role => role.Permissions)
            .Select(permission => permission.Name)
            .Where(permissionName => distinctRequiredPermissions.Contains(permissionName))
            .Distinct();

        if (logicalOperation is LogicalOperation.And)
        {
            var userPermissions = await userPermissionsQueryable
                .CountAsync();

            return userPermissions == distinctRequiredPermissions.Length;
        }

        if (logicalOperation is LogicalOperation.Or)
        {
            return await userPermissionsQueryable.AnyAsync();
        }

        return false;
    }

    public async Task<bool> HasRolesAsync(UserId userId, Role[] requiredRoles)
    {
        var distinctRequiredRoles = requiredRoles
            .Select(x => $"{x}")
            .Distinct()
            .ToArray();

        var userRolesCount = await _dbContext
            .Set<User>()
            .Include(x => x.Roles)
            .Where(x => x.Id == userId)
            .SelectMany(user => user.Roles)
            .Where(role => distinctRequiredRoles.Contains(role.Name))
            .Distinct()
            .CountAsync();

        return userRolesCount == distinctRequiredRoles.Length;
    }
}
