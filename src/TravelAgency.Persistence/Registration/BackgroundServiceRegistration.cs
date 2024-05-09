using Microsoft.Extensions.DependencyInjection;
using Quartz;
using TravelAgency.Persistence.BackgroundJobs;
using TravelAgency.Persistence.BackgroundJobs;

namespace TravelAgency.Persistence.Registration;

internal static class BackgroundServiceRegistration
{
    internal static IServiceCollection RegisterBackgroundServices(this IServiceCollection services)
    {
        services.AddScoped<IJob, ProcessOutboxMessagesJob>();
        services.AddScoped<IJob, DeleteOutdatedSoftDeletableEntitiesJob>();
        services.AddQuartz();

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        services.ConfigureOptions<QuartzOptionsSetup>();

        return services;
    }
}
