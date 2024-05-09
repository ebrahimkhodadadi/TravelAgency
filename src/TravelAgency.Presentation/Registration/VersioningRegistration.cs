using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace TravelAgency.Presentation.Registration;

internal static class VersioningRegistration
{
    private const string ApiVersionHeader = "api-version";

    internal static IServiceCollection RegisterVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = ApiVersion.Default;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new HeaderApiVersionReader(ApiVersionHeader);
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}