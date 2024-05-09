using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Tests.Performance.Client;
using TravelAgency.Tests.Performance.Options;
using TravelAgency.Tests.Performance.Persistence;

namespace TravelAgency.Tests.Performance;

public sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("performancesettings.json")
            .AddEnvironmentVariables()
            .Build();

        services.AddSingleton<DatabaseFixture>()
            .RegisterTestClient(configuration);

        services.Configure<PerformanceTestOptions>(configuration.GetSection(nameof(PerformanceTestOptions)));
    }
}