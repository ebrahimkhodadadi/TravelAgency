using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Application.Exceptions;
using TravelAgency.Persistence.Exceptions;
using TravelAgency.Infrastructure.Policies;

namespace TravelAgency.Persistence.Utilities;

public static class MigrationsUtilities
{
    internal static IApplicationBuilder ApplyMigrations<TDbContext>(this IApplicationBuilder app)
        where TDbContext : DbContext
    {
        var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

        using var applyMigrationsScope = serviceScopeFactory.CreateScope();

        var dbContext = applyMigrationsScope.ServiceProvider.GetService<TDbContext>();

        if (dbContext is null)
        {
            throw new UnavailableException("Database is not available");
        }

        var pendingMigrations = dbContext.Database.GetPendingMigrations();

        if (pendingMigrations.Any())
        {
            var result = PollyPipelines.MigrationRetryPipeline.ExecuteAndReturnResult(() => dbContext.Database.Migrate());

            if (result.IsFailure)
            {
                throw new MigrationException(result.Error);
            }
        }

        return app;
    }
}
