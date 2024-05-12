using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Domain.Billing.Repositories;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;
using TravelAgency.Domain.Users;
using TravelAgency.Infrastructure.Outbox;
using TravelAgency.Persistence.Repositories;

namespace TravelAgency.Persistence.Registration;

internal static class RepositoriesRegistration
{
    internal static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        //services.AddRepositories<CustomerRespository>();

        services.AddScoped<IBillRepository, BillRepository>();
        services.AddScoped<ICustomerRepository, CustomerRespository>();
        services.AddScoped<ITravelRepository, TravelRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IDiscountLogRepository, DiscountLogRepository>();

        services.AddScoped<IOutboxRepository, OutboxRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();

        return services;
    }

    //public static void AddRepositories<T>(this IServiceCollection services)
    //{
    //    services.Scan(scan => scan
    //        .FromAssemblyOf<T>()
    //        .AddClasses(classes => classes.AssignableTo<IRepository>())
    //        .AsMatchingInterface()
    //        .WithScopedLifetime());
    //}
}
