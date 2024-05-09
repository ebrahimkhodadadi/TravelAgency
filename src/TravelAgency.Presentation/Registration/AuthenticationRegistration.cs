using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Presentation.Authentication.ApiKeyAuthentication.Handlers;
using TravelAgency.Presentation.Authentication.RolePermissionAuthentication.Handlers;

namespace TravelAgency.Presentation.Registration;

internal static class AuthenticationRegistration
{
    internal static IServiceCollection RegisterAuthentication(this IServiceCollection services)
    {

        services.AddHttpContextAccessor();

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();

        services.AddAuthorization(options =>
        {
        });

        services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, RoleRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, ApiKeyRequirementHandler>();

        return services;
    }
}