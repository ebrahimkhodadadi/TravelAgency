using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Application.Mappings;
using TravelAgency.Application.Utilities;
using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Billing.Repositories;

namespace TravelAgency.Application.Features.Bills.Commands.Create;

internal sealed class CreateTravelCommandHandler(ITravelRepository travelRepository, IBillRepository billRepository, IValidator validator)
: ICommandHandler<CreateTravelCommand, CreateTravelResponse>
{
    private readonly ITravelRepository _travelRepository = travelRepository;
    private readonly IBillRepository _billRepository = billRepository;
    private readonly IValidator _validator = validator;

    public async Task<IResult<CreateTravelResponse>> Handle(CreateTravelCommand command, CancellationToken cancellationToken)
    {
        var billId = BillId.Create(command.billId);
        var bill = await _billRepository.GetByIdAsync(billId);
        var travel = Travel.Create(command.direction, command.start, command.type, command.price, billId);
        var travelResult = bill.CreateTravel(travel);

        _validator
            .Validate(travelResult);
        if (_validator.IsInvalid)
        {
            return _validator
                .Failure<CreateTravelResponse>()
                .ToResult();
        }

        return travel
            .ToCreateResponse()
            .ToResult();
    }
}