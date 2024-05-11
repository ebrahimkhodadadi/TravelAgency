using TravelAgency.Application.Features.Bills.Commands.Create;

namespace TravelAgency.Presentation.Controllers;

public sealed class TravelController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    [ProducesResponseType<CreateTravelResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok<CreateTravelResponse>, ProblemHttpResult>> CreateTravel
        (
        [FromBody] CreateTravelCommand command,
        CancellationToken cancellationToken
        )
    {
        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return TypedResults.Ok(result.Value);
    }
}