﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;
using TravelAgency.Domain.Users;
using TravelAgency.Infrastructure.Options;
using TravelAgency.Persistence.Framework;
using TravelAgency.Persistence.Registration;
using static Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus;

namespace TravelAgency.Persistence.Registration;

//To add other custom health checks, use AddCheck method and as a generic parameter pass a class that implements IHealthCheck interface
internal static class HealthCheckRegistration
{
    private const string Basic = nameof(Basic);
    private const string Critical = nameof(Critical);
    private const string Readiness = nameof(Readiness);

    internal static IServiceCollection RegisterHealthChecks(this IServiceCollection services)
    {
        var healthOptions = services.GetOptions<HealthOptions>();
        var databaseOptions = services.GetOptions<DatabaseOptions>();

        services
            .AddHealthChecks()
            .AddSqlServer
            (
                connectionString: databaseOptions.ConnectionString!,
                failureStatus: Unhealthy,
                healthQuery: "SELECT 1",
                name: "SqlServer connection",
                tags: new[] { Basic, Critical }
            )
            .AddDbContextCheck<TravelAgencyDbContext>
            (
                failureStatus: Unhealthy,
                name: "DbContext readiness",
                customTestQuery: GetUser,
                tags: new[] { Readiness }
            )
            .AddCheck<AppHealthCheck>
            (
                failureStatus: Unhealthy,
                name: "Application status",
                tags: new[] { Basic, Critical }
            );

        services
            .Configure<HealthCheckPublisherOptions>(options =>
            {
                options.Delay = TimeSpan.FromSeconds(healthOptions.DelayInSeconds);
                options.Period = TimeSpan.FromSeconds(healthOptions.PeriodInSeconds);
                options.Predicate = (check) => check.Tags.Contains(Basic);
            });

        return services;
    }

    internal static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/api/health", new HealthCheckOptions
        {
            ResultStatusCodes =
            {
                [Healthy] = StatusCodes.Status200OK,
                [Degraded] = StatusCodes.Status200OK,
                [Unhealthy] = StatusCodes.Status503ServiceUnavailable,
            },
            ResponseWriter = WriteResponse,
            Predicate = c => c.Tags.Contains(Basic)
        });

        app.UseHealthChecks("/api/readiness", new HealthCheckOptions
        {
            ResultStatusCodes =
            {
                [Healthy] = StatusCodes.Status200OK,
                [Degraded] = StatusCodes.Status200OK,
                [Unhealthy] = StatusCodes.Status503ServiceUnavailable,
            },
            ResponseWriter = WriteResponse,
            Predicate = c => c.Tags.Contains(Readiness)
        });

        return app;
    }

    private static async Task<bool> GetUser(TravelAgencyDbContext context, CancellationToken cancellationToken)
    {
        var products = await context
            .Set<User>()
            .OrderBy(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        return products is not null;
    }

    private static Task WriteResponse(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = "application/json";

        var healthCheckDictionary = report
            .Entries
            .Select(e => (e.Key, new
            {
                status = e.Value.Status.ToString(),
                exception = e.Value.Exception?.Message,
                duration = e.Value.Duration.ToString(),
                description = e.Value.Description,
                data = e.Value.Data.Select(p => new
                {
                    p.Key,
                    p.Value
                })
            }))
            .ToDictionary(x => x.Key, x => x.Item2);

        var json = JsonSerializer.Serialize
        (
            new
            {
                status = report.Status.ToString(),
                checks = healthCheckDictionary
            }
        );

        return context
            .Response
            .WriteAsync(json);
    }
}

public class AppHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy("An application is running."));
    }
}