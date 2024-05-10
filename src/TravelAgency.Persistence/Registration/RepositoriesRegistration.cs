using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Domain.Billing.Repositories;
using TravelAgency.Domain.Users;
using TravelAgency.Infrastructure.Outbox;
using TravelAgency.Persistence.Repositories;

namespace TravelAgency.Persistence.Registration;

internal static class RepositoriesRegistration
{
    internal static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBillRepository, BillRepository>();

        services.AddScoped<IOutboxRepository, OutboxRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();

        return services;
    }
}
