using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Application.Features.Bills.Commands.Create;
using TravelAgency.Presentation.Abstractions;

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
}