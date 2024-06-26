﻿using Serilog;
using static Serilog.Events.LogEventLevel;

namespace TravelAgency.App.Utilities;

public static class LoggerUtilities
{
    private const string Microsoft = nameof(Microsoft);

    public static Serilog.ILogger CreateSerilogLogger()
    {
        return new LoggerConfiguration()
            .MinimumLevel.Override(Microsoft, Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();
    }

    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext());
    }
}
