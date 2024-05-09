using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using TravelAgency.Tests.Integration.Container.Api;
using TravelAgency.Tests.Integration.Container.Configurations;
using TravelAgency.Tests.Integration.Container.Persistance;
using TravelAgency.Tests.Integration.Container.Utilities;
using static RestSharp.Method;
using static TravelAgency.Tests.Integration.Container.Constants.Constants.Header;
using static TravelAgency.Tests.Integration.Container.Constants.Constants.IntegrationTest;

namespace TravelAgency.Tests.Integration.Container.Abstractions;

public abstract class ControllerTestsBase
{
    protected HttpClient httpClient;
    protected readonly string TravelAgencyApiUrl;
    protected readonly string controllerUrl;
    protected readonly DatabaseFixture fixture;

    public ControllerTestsBase(TravelAgencyApiFactory apiFactory)
    {
        TravelAgencyApiUrl = apiFactory.Services.GetRequiredService<IntegrationTestsUrlOptions>().TravelAgencyApiUrl!;
        controllerUrl = GetType().Name[..^ControllerTests.Length];

        httpClient = apiFactory.CreateClient();
        fixture = new DatabaseFixture(apiFactory.ContainerConnectionString);
    }

    /// <summary>
    /// Create the rest client with api url appended by given controller url and ensure that the test user for this client has all privileges
    /// </summary>
    /// <param name="httpClient">Controller url</param>
    /// <param name="databaseFixture">Database fixture</param>
    /// <returns></returns>
    protected static RestClient RestClient(HttpClient httpClient, RestClientOptions restClientOptions)
    {
        var client = new RestClient(httpClient, restClientOptions);
        EnsureApiKeyAuthenticationForMockedIApiKeyService(client);
        return client;
    }

    /// <summary>
    /// Get Request 
    /// </summary>
    /// <param name="endpointUri">Endpoint</param>
    /// <returns></returns>
    protected static RestRequest GetRequest(string endpointUri)
    {
        return new RestRequest(endpointUri, Get);
    }

    /// <summary>
    /// Post Request
    /// </summary>
    /// <param name="endpointUri">Endpoint uri</param>
    /// <param name="body">Request body</param>
    /// <returns></returns>
    protected static RestRequest PostRequest(string endpointUri, object body)
    {
        var request = new RestRequest(endpointUri, Post);

        return request
            .AddJson(body);
    }

    /// <summary>
    /// Patch Request
    /// </summary>
    /// <param name="endpointUri">Endpoint uri</param>
    /// <param name="body">Request body</param>
    /// <returns></returns>
    protected static RestRequest PatchRequest(string endpointUri, object body)
    {
        var request = new RestRequest(endpointUri, Patch);

        return request
            .AddJson(body);
    }

    /// <summary>
    /// Delete Request
    /// </summary>
    /// <param name="endpointUri">Endpoint uri</param>
    /// <returns></returns>
    protected static RestRequest DeleteRequest(string endpointUri)
    {
        return new RestRequest(endpointUri, Delete);
    }

    /// <summary>
    /// This method adds api key header with dummy value for every request. IApiKeyService should be mocked in TravelAgencyApiFactory to return true for each validation. 
    /// </summary>
    /// <param name="client">RestClient that will be used for tests</param>
    private static void EnsureApiKeyAuthenticationForMockedIApiKeyService(RestClient client)
    {
        client.AddDefaultHeader(ApiKeyHeader, nameof(ApiKeyHeader));
    }
}