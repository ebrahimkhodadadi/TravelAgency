using TravelAgency.Application.Abstractions.CQRS;

namespace TravelAgency.Application.Features.Bills.Commands.Create
{
    public sealed record CreateBillCommand(
            Ulid customerId
        ) : ICommand<CreateBillResponse>;
}
