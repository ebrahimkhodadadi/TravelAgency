using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Application.Middlewares;

namespace TravelAgency.Application.Registration;

internal static class MiddlewaresRegistration
{
    internal static IServiceCollection RegisterMiddlewares(this IServiceCollection services)
    {
        //Order is not important
        services.AddScoped<ErrorHandlingMiddleware>();
        services.AddScoped<RequestTimeMiddleware>();

        return services;
    }

    internal static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
    {
        //Order is important
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseMiddleware<RequestTimeMiddleware>();

        return app;
    }
}