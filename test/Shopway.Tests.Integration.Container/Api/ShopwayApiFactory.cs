using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using TravelAgency.Tests.Integration.Configurations;
using Testcontainers.MsSql;
using TravelAgency.App.wwwroot;
using TravelAgency.Application.Abstractions;
using TravelAgency.Infrastructure.Options;
using TravelAgency.Persistence.Framework;
using TravelAgency.Presentation.Authentication.ApiKeyAuthentication;
using TravelAgency.Presentation.Authentication.RolePermissionAuthentication;

namespace TravelAgency.Tests.Integration.Container.Api;

public sealed class TravelAgencyApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    public string ContainerConnectionString { get; private set; } = string.Empty;

    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
        .Build();

    public TravelAgencyApiFactory()
    {
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        //Set container connection string
        ContainerConnectionString = _msSqlContainer.GetConnectionString();

        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });

        builder.ConfigureTestServices(services =>
        {
            //Remove background workers
            services.RemoveAll(typeof(IHostedService));
            services.RemoveAll(typeof(IJob));

            //Register options
            services.AddSingleton(x => new IntegrationTestsUrlOptions()
            {
                TravelAgencyApiUrl = "https://localhost:7236/api/"
            });

            //Mock api key authentication
            services.RemoveAll(typeof(IApiKeyService));
            services.AddScoped<IApiKeyService, TestApiKeyService>();

            //Mock jwt authentication
            services.RemoveAll(typeof(IUserAuthorizationService));
            services.AddScoped<IUserAuthorizationService, TestUserAuthorizationService>();

            //Mock user context
            services.RemoveAll(typeof(IUserContextService));
            services.AddScoped<IUserContextService, TestUserContextService>();

            //Add AuthorizationHandlers that will succeed all requirements
            services.RemoveAll<IAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, TestPermissionRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, TestRoleRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, TestApiKeyRequirementHandler>();

            //Re-register database context to use the connection to the database in the container
            services.Configure<DatabaseOptions>(options =>
            {
                options.ConnectionString = ContainerConnectionString;
            });

            services.RemoveAll(typeof(TravelAgencyDbContext));
            services.RegisterDatabaseContext(true);
        });
    }

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();
    }

    //This method from the interface IAsyncLifetime will hide the one in the "WebApplicationFactory<IApiMarker>"
    public new async Task DisposeAsync()
    {
        await _msSqlContainer.DisposeAsync();
    }
}