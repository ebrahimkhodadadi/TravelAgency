using FluentValidation;
using MediatR;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Results.Abstractions;
using TravelAgency.Domain.Common.Utilities;

namespace TravelAgency.Application.Pipelines;

public sealed class FluentValidationPipeline<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class, IResult
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle
    (
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        if (_validators.IsEmpty())
        {
            return await next();
        }

        Error[] errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => Error.New(failure.PropertyName, failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Length is not 0)
        {
            return errors.CreateValidationResult<TResponse>();
        }

        return await next();
    }
}
