using Microsoft.Extensions.Configuration;
using static TravelAgency.Presentation.Authentication.ApiKeyAuthentication.Constants.ApiKey;

namespace TravelAgency.Presentation.Authentication.ApiKeyAuthentication;

public sealed class ApiKeyService(IConfiguration configuration) : IApiKeyService
{
    private readonly IConfiguration _configuration = configuration;

    public bool IsProvidedApiKeyEqualToRequiredApiKey(RequiredApiKey requiredApiKey, string? apiKeyFromHeader)
    {
        //For tutorial purpose, api keys are stored in appsettings
        var requiredApiKeyValue = _configuration
            .GetValue<string>($"{ApiKeySection}:{requiredApiKey}")!;

        return requiredApiKeyValue.Equals(apiKeyFromHeader);
    }
}