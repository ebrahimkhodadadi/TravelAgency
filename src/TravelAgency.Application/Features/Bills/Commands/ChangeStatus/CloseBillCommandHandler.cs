using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Billing.Repositories;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Results;

namespace TravelAgency.Application.Features.Bills.Commands.ChangeStatus;

internal sealed class CloseBillCommandHandler(IBillRepository billRepository, IValidator validator)
: ICommandHandler<CloseBillCommand>
{
    private readonly IBillRepository _billRepository = billRepository;
    private readonly IValidator _validator = validator;

    public async Task<IResult> Handle(CloseBillCommand command, CancellationToken cancellationToken)
    {
        var billId = BillId.Create(command.billId);
        var bill = await _billRepository.GetByIdAsync(billId);
        _validator
            .If(bill is null, Error.NotFound<Bill>(billId));
        if (_validator.IsInvalid)
            return _validator.Failure();

        var paymentResult = bill.Close();

        _validator
            .Validate(paymentResult);
        if (_validator.IsInvalid)
        {
            return _validator
                .Failure();
        }

        return Result.Success();
    }
}