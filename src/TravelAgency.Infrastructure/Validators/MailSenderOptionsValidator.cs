using Microsoft.Extensions.Options;
using TravelAgency.Domain.Common.Utilities;
using TravelAgency.Infrastructure.Options;

namespace TravelAgency.Infrastructure.Validators;

public sealed class MailSenderOptionsValidator : IValidateOptions<MailSenderOptions>
{
    public ValidateOptionsResult Validate(string? name, MailSenderOptions options)
    {
        var validationResult = string.Empty;

        if (options.Host.IsNullOrEmptyOrWhiteSpace())
        {
            validationResult += "Host is missing. ";
        }

        if (options.Port <= 0)
        {
            validationResult += "Invalid port.";
        }

        if (!validationResult.IsNullOrEmptyOrWhiteSpace())
        {
            return ValidateOptionsResult.Fail(validationResult);
        }

        return ValidateOptionsResult.Success;
    }
}