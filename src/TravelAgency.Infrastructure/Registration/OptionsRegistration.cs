using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TravelAgency.Infrastructure.Options;
using TravelAgency.Infrastructure.Validators;

namespace TravelAgency.Infrastructure.Registration;

internal static class OptionsRegistration
{
    internal static IServiceCollection RegisterOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<DatabaseOptionsSetup>();
        services.ConfigureOptions<CacheOptionsSetup>();
        services.ConfigureOptions<AuthenticationOptionsSetup>();
        services.ConfigureOptions<BearerAuthenticationOptionsSetup>();
        services.ConfigureOptions<HealthCheckOptionsSetup>();
        services.ConfigureOptions<MailSenderOptionsSetup>();

        services.AddSingleton<IValidateOptions<DatabaseOptions>, DatabaseOptionsValidator>();
        services.AddSingleton<IValidateOptions<AuthenticationOptions>, AuthenticationOptionsValidator>();
        services.AddSingleton<IValidateOptions<HealthOptions>, HealthOptionsValidator>();
        services.AddSingleton<IValidateOptions<CacheOptions>, CacheOptionsValidator>();
        services.AddSingleton<IValidateOptions<MailSenderOptions>, MailSenderOptionsValidator>();

        return services;
    }
}
