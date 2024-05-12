using TravelAgency.Application.Features.Payments.Commands.Create;

namespace TravelAgency.Presentation.Controllers;

public sealed class PaymentController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    [ProducesResponseType<CreatePaymentResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok<CreatePaymentResponse>, ProblemHttpResult>> CreatePayment
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
    
    [HttpPost("Transfer")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok, ProblemHttpResult>> TransferPayment
        (
        [FromBody] TransferPaymentCommand command,
        CancellationToken cancellationToken
        )
    {
        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return TypedResults.Ok();
    }

    [HttpDelete]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok, ProblemHttpResult>> DeletePayment
    (
    Ulid Id,
    CancellationToken cancellationToken
    )
    {
        var result = await Sender.Send(new DeletePaymentCommand(Id), cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return TypedResults.Ok();
    }
}
