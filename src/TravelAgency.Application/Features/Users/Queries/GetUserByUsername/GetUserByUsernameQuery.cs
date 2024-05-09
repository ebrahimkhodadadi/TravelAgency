using TravelAgency.Application.Abstractions.CQRS;

namespace TravelAgency.Application.Features.Users.Queries.GetUserByUsername;

public sealed record GetUserByUsernameQuery(string Username) : ICachedQuery<UserResponse>
{
    public string CacheKey => Username;

    public TimeSpan? Duration => TimeSpan.FromMinutes(1);
}