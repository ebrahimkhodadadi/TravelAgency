using TravelAgency.Application.Features.Payments.Commands.Create;
using TravelAgency.Application.Features.Travels.Commands.Cancel;
using TravelAgency.Domain.Billing;

namespace TravelAgency.Presentation.Controllers;

public sealed class TravelController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    [ProducesResponseType<CreatePaymentResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok<CreatePaymentResponse>, ProblemHttpResult>> CreateTravel
        (
        [FromBody] CreatePaymentCommand command,
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

    [HttpPut("Cancel")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok, ProblemHttpResult>> CancelTravel
    (
    Ulid Id,
    CancellationToken cancellationToken
    )
    {
        var result = await Sender.Send(new CancelTravelCommand(Id), cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return TypedResults.Ok();
    }
}