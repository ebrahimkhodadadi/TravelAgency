using Newtonsoft.Json;
using System.Text;

namespace TravelAgency.Tests.Performance.Utilities;

public static class RequestUtilities
{
    public static StringContent ToStringContent<TBody>(this TBody body)
    {
        var bodyAsJson = JsonConvert.SerializeObject(body);
        return new StringContent(bodyAsJson, Encoding.UTF8, Constants.Constants.Http.JsonMediaType);
    }
}