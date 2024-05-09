using TravelAgency.Domain.Common.Errors;

namespace TravelAgency.Domain.Common.Results.Abstractions;

public interface IResult<out TValue> : IResult
{
    TValue Value { get; }
}

public interface IResult
{
    bool IsSuccess { get; }

    bool IsFailure { get; }

    Error Error { get; }
}