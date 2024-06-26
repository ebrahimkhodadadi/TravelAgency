﻿using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Users;
using TravelAgency.Domain.Users.Enumerations;
using TravelAgency.Domain.Users.ValueObjects;
using TravelAgency.Persistence.Framework;

namespace TravelAgency.Persistence.Repositories;

public sealed class UserRepository(TravelAgencyDbContext dbContext) : IUserRepository
{
    private readonly TravelAgencyDbContext _dbContext = dbContext;

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken)
    {
        return await _dbContext
            .Set<User>()
            .Where(user => user.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        return await _dbContext
            .Set<User>()
            .Where(user => user.Email == email)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken)
    {
        return await IsEmailTakenAsync(email, cancellationToken) is false;
    }

    public async Task<bool> IsEmailTakenAsync(Email email, CancellationToken cancellationToken)
    {
        return await _dbContext
            .Set<User>()
            .Where(user => user.Email == email)
            .AnyAsync(cancellationToken);
    }

    public void Add(User user)
    {
        _dbContext
            .Set<User>()
            .Add(user);
    }

    public void Update(User user)
    {
        _dbContext
            .Set<User>()
            .Update(user);
    }

    public async Task<User?> GetByUsernameAsync(Username username, CancellationToken cancellationToken)
    {
        return await _dbContext
            .Set<User>()
            .Include(user => user.Roles)
                .ThenInclude(role => role.Permissions)
            .Where(user => user.Username == username)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Role?> GetRolePermissionsAsync(Role role, CancellationToken cancellationToken)
    {
        return await _dbContext
            .Set<Role>()
                .Include(x => x.Permissions)
            .Where(x => x.Id == role.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
