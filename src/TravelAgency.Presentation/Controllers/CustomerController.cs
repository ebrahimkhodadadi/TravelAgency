using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Presentation.Abstractions;
using TravelAgency.Application.Features.Customers.Commands.Create;

namespace TravelAgency.Presentation.Controllers
{
    public sealed class CustomerController(ISender sender) : ApiController(sender)
    {
        [HttpPost]
        [ProducesResponseType<CreateCustomerResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<Results<Ok<CreateCustomerResponse>, ProblemHttpResult>> CreateCustomer
            (
            [FromBody] CreateCustomerCommand command, 
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
}
