using TravelAgency.Domain.Common.Errors;

namespace TravelAgency.Tests.Integration.Container.ControllersUnderTest;

/// <summary>
/// Represents a helper class used to deserialize the validation problem details
/// </summary>
public sealed class ValidationProblemDetails
{
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Status { get; set; }
    public string Detail { get; set; } = string.Empty;
    public List<Error> Errors { get; set; } = [];
}