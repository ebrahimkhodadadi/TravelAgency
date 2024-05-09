using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Application.Features.Users.Commands.LogUser;
using TravelAgency.Tests.Performance.Abstractions;
using TravelAgency.Tests.Performance.Utilities;

namespace TravelAgency.Tests.Performance.Client;

public static class ClientRegistration
{
    private const string ApplicationUrl = "ApiUrl";
    private const string SingleFactorAuthorizationEndpoint = "users/login";

    public static IServiceCollection RegisterTestClient(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpClient<PerformanceTestsBase>(async (httpClient) =>
            {
                httpClient.BaseAddress = new Uri(configuration.GetValue<string>(ApplicationUrl)!);
                await httpClient
                    .WithBearerToken
                    (
                        SingleFactorAuthorizationEndpoint,
                        new LogUserCommand(Constants.Constants.TestUser.Email, Constants.Constants.TestUser.Password).ToStringContent()
                    );
            })
            .ConfigurePrimaryHttpMessageHandler(x =>
            {
                return new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    }
                };
            });

        return services;
    }
}