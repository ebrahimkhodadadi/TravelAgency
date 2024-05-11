using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Application.Mappings;
using TravelAgency.Application.Utilities;
using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Billing.Repositories;
using TravelAgency.Domain.Users;

namespace TravelAgency.Application.Features.Bills.Commands.Create;

internal sealed class CreateBillCommandHandler(IBillRepository billRepository, IValidator validator)
: ICommandHandler<CreateBillCommand, CreateBillResponse>
{
    private readonly IBillRepository _billRepository = billRepository;
    private readonly IValidator _validator = validator;

    public async Task<IResult<CreateBillResponse>> Handle(CreateBillCommand command, CancellationToken cancellationToken)
    {
        var bill = Bill.Create(CustomerId.Create(command.customerId));
        _billRepository.Add(bill);

        return bill
            .ToCreateResponse()
            .ToResult();
    }
}