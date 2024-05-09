using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Application.Cache;
using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Application.Features.Proxy;

namespace TravelAgency.Application.Registration;

public static class ApplicationLayerRegistration
{
    public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
    {
        Console.WriteLine($"Seeding Application Layer Memory Cache: {ApplicationCache.SeedCache}");

        services.AddScoped<IMediatorProxyService, MediatorProxyService>();

        services
            .RegisterFluentValidation()
            .RegisterMiddlewares();

        return services;
    }

    public static IApplicationBuilder UseApplicationLayer(this IApplicationBuilder app)
    {
        app
            .UseMiddlewares();

        return app;
    }
}