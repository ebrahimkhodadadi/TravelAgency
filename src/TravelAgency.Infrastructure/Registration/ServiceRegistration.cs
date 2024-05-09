using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using TravelAgency.Infrastructure.Builders.Batch;
using TravelAgency.Infrastructure.Services;
using TravelAgency.Application.Abstractions;
using TravelAgency.Application.Abstractions.CQRS.Batch;
using TravelAgency.Domain.Users;
using TravelAgency.Infrastructure;
using TravelAgency.Infrastructure.Services;
using TravelAgency.Infrastructure.Validators;

namespace TravelAgency.Infrastructure.Registration;

internal static class ServiceRegistration
{
    internal static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        //Services

        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IUserContextService, UserContextService>();
        services.AddScoped<IEmailSender, MimeKitEmailSender>();
        services.AddScoped<IToptService, ToptService>();

        //Validators

        services.AddScoped<IValidator, Validator>();

        //Builders

        services.AddScoped(typeof(IBatchResponseBuilder<,>), typeof(BatchResponseBuilder<,>));

        //Scan for the rest

        services.Scan(selector => selector
            .FromAssemblies(
                AssemblyReference.Assembly)
            .AddClasses(false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());

        return services;
    }
}
