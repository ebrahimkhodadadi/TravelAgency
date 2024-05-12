using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Application.Mappings;
using TravelAgency.Application.Utilities;
using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Billing.Repositories;
using TravelAgency.Domain.Common.Errors;

namespace TravelAgency.Application.Features.Travels.Commands.Create;

internal sealed class CancelTravelCommandHandler(ITravelRepository travelRepository, IBillRepository billRepository, IValidator validator)
: ICommandHandler<CancelTravelCommand, CancelTravelResponse>
{
    private readonly ITravelRepository _travelRepository = travelRepository;
    private readonly IBillRepository _billRepository = billRepository;
    private readonly IValidator _validator = validator;

    public async Task<IResult<CancelTravelResponse>> Handle(CancelTravelCommand command, CancellationToken cancellationToken)
    {
        var billId = BillId.Create(command.billId);
        var bill = await _billRepository.GetByIdAsync(billId);
        _validator
            .If(bill is null, Error.NotFound<Bill>(billId));
        if (_validator.IsInvalid)
            return _validator.Failure<CancelTravelResponse>();

        if (command.payment != null)
        {
            var paymentResult = bill.Pay(command.price, command.payment.paymentType);

            _validator
                .Validate(paymentResult);
            if (_validator.IsInvalid)
            {
                return _validator
                    .Failure<CancelTravelResponse>()
                    .ToResult();
            }
        }

        var travel = Travel.Create(command.direction, command.start, command.type, command.price, billId);
        var travelResult = bill.CreateTravel(travel);

        _validator
            .Validate(travelResult);
        if (_validator.IsInvalid)
        {
            return _validator
                .Failure<CancelTravelResponse>()
                .ToResult();
        }

        return travel
            .ToCreateResponse()
            .ToResult();
    }
}