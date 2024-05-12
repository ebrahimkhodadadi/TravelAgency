using TravelAgency.Application.Features.Bills.Commands.ChangeStatus;
using TravelAgency.Application.Features.Bills.Commands.Create;

namespace TravelAgency.Presentation.Controllers;

public sealed class BillController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    [ProducesResponseType<CreateBillResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok<CreateBillResponse>, ProblemHttpResult>> CreateBill
        (
        [FromBody] CreateBillCommand command,
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
    
    [HttpPut("Close")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<Results<Ok, ProblemHttpResult>> CloseBill
        (
        Ulid Id,
        CancellationToken cancellationToken
        )
    {
        var result = await Sender.Send(new CloseBillCommand(Id), cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return TypedResults.Ok();
    }
}