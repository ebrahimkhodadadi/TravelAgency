using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Application.Mappings;
using TravelAgency.Application.Utilities;
using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Billing.Repositories;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Results;

namespace TravelAgency.Application.Features.Travels.Commands.Cancel;

internal sealed class CancelTravelCommandHandler(ITravelRepository travelRepository, IPaymentRepository paymentRepository, IValidator validator)
: ICommandHandler<CancelTravelCommand>
{
    private readonly ITravelRepository _travelRepository = travelRepository;
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IValidator _validator = validator;

    public async Task<IResult> Handle(CancelTravelCommand command, CancellationToken cancellationToken)
    {
        var travelId = TravelId.Create(command.travelId);
        var travel = await _travelRepository.GetByIdAsync(travelId);
        _validator
            .If(travel is null, Error.NotFound<Travel>(travelId));
        if (_validator.IsInvalid)
            return _validator.Failure();

        var result = travel.Cancel(_paymentRepository);

        _validator
            .Validate(result);
        if (_validator.IsInvalid)
        {
            return _validator
                .Failure();
        }

        return Result.Success();
    }
}