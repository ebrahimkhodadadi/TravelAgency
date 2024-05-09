using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scrutor;
using TravelAgency.Presentation;

namespace TravelAgency.Presentation.Registration;

public static class PresentationLayerRegistration
{
    public static IServiceCollection RegisterPresentationLayer(this IServiceCollection services)
    {
        services
            .RegisterControllers()
            .RegisterOpenApi()
            .RegisterVersioning()
            .RegisterAuthentication();

        services.Scan(selector => selector
            .FromAssemblies(
                AssemblyReference.Assembly)
            .AddClasses(false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());

        return services;
    }

    public static IApplicationBuilder UsePresentationLayer(this IApplicationBuilder app, IHostEnvironment environment)
    {
        app
            .ConfigureOpenApi(environment.IsDevelopment())
            .UseAuthorization()
            .UseCorsPolicy();

        return app;
    }
}