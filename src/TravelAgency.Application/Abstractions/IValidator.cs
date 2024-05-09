using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Results;

namespace TravelAgency.Application.Abstractions;

public interface IValidator
{
    bool IsValid { get; }
    bool IsInvalid { get; }

    IValidator If(bool condition, Error thenError);
    IValidator Validate<TType>(Result<TType> result);
    IValidator Validate<TType>(ValidationResult<TType> validationResult);
    ValidationResult<TResponse> Failure<TResponse>()
            where TResponse : IResponse;
    ValidationResult Failure();
    IValidator Validate(Result result);
}