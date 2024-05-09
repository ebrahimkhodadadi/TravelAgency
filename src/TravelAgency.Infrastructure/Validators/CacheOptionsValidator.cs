using Microsoft.Extensions.Options;
using TravelAgency.Domain.Common.Utilities;
using TravelAgency.Infrastructure.Options;

namespace TravelAgency.Infrastructure.Validators;

public sealed class CacheOptionsValidator : IValidateOptions<CacheOptions>
{
    public ValidateOptionsResult Validate(string? name, CacheOptions options)
    {
        var validationResult = string.Empty;

        if (options.ConnectionString.IsNullOrEmptyOrWhiteSpace())
        {
            validationResult += "Connection string is missing. ";
        }

        if (!validationResult.IsNullOrEmptyOrWhiteSpace())
        {
            return ValidateOptionsResult.Fail(validationResult);
        }

        return ValidateOptionsResult.Success;
    }
}
