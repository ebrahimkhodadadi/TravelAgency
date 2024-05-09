using System.Net;

namespace TravelAgency.Tests.Integration.Container.WireMockUtilities.ResponseProviders;

public sealed record ResponseEntry(HttpStatusCode HttpStatusCode, object? Body = default)
{
    public bool HasBody => Body is not null;
}
