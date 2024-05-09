using TravelAgency.Presentation.Authentication.ApiKeyAuthentication;

namespace TravelAgency.Tests.Integration.Container.Api;

public sealed class TestApiKeyService : IApiKeyService
{
    public bool IsProvidedApiKeyEqualToRequiredApiKey(RequiredApiKey requiredApiKey, string? apiKeyFromHeader)
    {
        return true;
    }
}