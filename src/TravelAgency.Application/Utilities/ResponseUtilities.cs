using TravelAgency.Application.Abstractions;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Common.Results.Abstractions;

namespace TravelAgency.Application.Utilities;

public static class ResponseUtilities
{
    public static IResult<TResponse> ToResult<TResponse>(this TResponse response)
        where TResponse : class, IResponse
    {
        return Result.Create(response);
    }

    public static IResult<TResponse> ToResult<TResponse>(this ValidationResult<TResponse> response)
        where TResponse : class, IResponse
    {
        return response;
    }
}