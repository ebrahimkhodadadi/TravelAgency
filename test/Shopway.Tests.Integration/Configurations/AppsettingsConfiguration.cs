﻿using Microsoft.Extensions.Configuration;
using static TravelAgency.Tests.Integration.Constants.Constants.IntegrationTest;

namespace TravelAgency.Tests.Integration.Configurations;

public static class AppsettingsConfiguration
{
    private static IConfigurationRoot GetConfigurationRoot()
    {
        var builder = new ConfigurationBuilder()
            .AddEnvironmentVariables();

        var path = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;
        builder.AddJsonFile($@"{path}/{AppsettingsTestJson}");

        return builder.Build();
    }

    public static IConfiguration GetConfiguration()
    {
        return GetConfigurationRoot();
    }
}