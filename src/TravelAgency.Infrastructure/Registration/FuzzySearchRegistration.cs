using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Application.Abstractions;
using TravelAgency.Infrastructure.FuzzySearch;

namespace TravelAgency.Infrastructure.Registration;

internal static class FuzzySearchRegistration
{
    internal static IServiceCollection RegisterFuzzySearch(this IServiceCollection services)
    {
        services.AddScoped<IFuzzySearchFactory, SymSpellFuzzySearchFactory>();

        return services;
    }
}