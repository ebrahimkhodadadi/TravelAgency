﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TravelAgency.Infrastructure.Options;
using TravelAgency.Persistence.Abstractions;
using TravelAgency.Persistence.Framework;

namespace TravelAgency.Persistence.Registration;

internal static class DatabaseContextRegistration
{
    internal static IServiceCollection RegisterDatabaseContext(this IServiceCollection services, bool isDevelopment)
    {
        services.AddDbContextPool<TravelAgencyDbContext>((serviceProvider, optionsBuilder) =>
        {
            var databaseOptions = services.GetOptions<DatabaseOptions>();

            optionsBuilder.UseSqlServer(databaseOptions.ConnectionString!, options =>
            {
                options.CommandTimeout(databaseOptions.CommandTimeout);

                options.EnableRetryOnFailure(
                    databaseOptions.MaxRetryCount,
                    TimeSpan.FromSeconds(databaseOptions.MaxRetryDelay),
                    Array.Empty<int>());
            });

            if (isDevelopment)
            {
                //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Debug);
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.EnableSensitiveDataLogging(); //DO NOT USE THIS IN PRODUCTION! Used to get parameter values. DO NOT USE THIS IN PRODUCTION!
                optionsBuilder.ConfigureWarnings(warningAction =>
                {
                    warningAction.Log(new EventId[]
                    {
                        CoreEventId.FirstWithoutOrderByAndFilterWarning,
                        CoreEventId.RowLimitingOperationWithoutOrderByWarning
                    });
                });
            }
        });

        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

        return services;
    }
}