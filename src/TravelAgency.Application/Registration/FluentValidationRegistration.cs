using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace TravelAgency.Application.Registration;

internal static class FluentValidationRegistration
{
    internal static IServiceCollection RegisterFluentValidation(this IServiceCollection services)
    {
        services
            .AddValidatorsFromAssembly
            (
                AssemblyReference.Assembly,
                includeInternalTypes: true
            );

        return services;
    }
}
