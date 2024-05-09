using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Domain.Common.Results.Abstractions;
using static TravelAgency.Application.Constants.Constants.ProblemDetails;
using static TravelAgency.Application.Utilities.ProblemDetailsUtilities;
using IResult = TravelAgency.Domain.Common.Results.Abstractions.IResult;

namespace TravelAgency.Presentation.Abstractions;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public abstract class ApiController(ISender sender) : ControllerBase
{
    protected readonly ISender Sender = sender;

    protected static ProblemHttpResult HandleFailure(IResult result)
    {
        return result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException("Result was successful"),

            IValidationResult validationResult => TypedResults.Problem
            (
                CreateProblemDetails
                (
                    ValidationError,
                    StatusCodes.Status400BadRequest,
                    result.Error,
                    validationResult.ValidationErrors
                )
            ),

            _ => TypedResults.Problem
            (
                CreateProblemDetails
                (
                    InvalidRequest,
                    StatusCodes.Status400BadRequest,
                    result.Error
                )
            )
        };
    }

    protected static CreatedAtRoute<T> CreatedAtActionResult<T>(IResult<T> result, string? routeName)
    {
        return TypedResults.CreatedAtRoute
        (
            result.Value,
            routeName,
            new { id = result.Value }
        );
    }
}
