using TravelAgency.Domain.Common.BaseTypes.Abstractions;
using TravelAgency.Domain.Users.Enumerations;
using TravelAgency.Domain.Users.ValueObjects;

namespace TravelAgency.Domain.Users;

public interface IUserRepository : IRepository
{
    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken);

    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken);

    Task<User?> GetByUsernameAsync(Username username, CancellationToken cancellationToken);

    Task<Role?> GetRolePermissionsAsync(Role role, CancellationToken cancellationToken);

    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken);

    Task<bool> IsEmailTakenAsync(Email email, CancellationToken cancellationToken);

    void Add(User user);

    void Update(User user);
}