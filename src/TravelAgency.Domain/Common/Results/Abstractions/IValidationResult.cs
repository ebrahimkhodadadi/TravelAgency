using TravelAgency.Domain.Common.Errors;

namespace TravelAgency.Domain.Common.Results.Abstractions;

public interface IValidationResult
{
    Error[] ValidationErrors { get; }
}
