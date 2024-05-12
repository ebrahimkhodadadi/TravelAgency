using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Billing.Enumerations;
using TravelAgency.Domain.Billing.Repositories;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Results;

namespace TravelAgency.Application.Features.Payments.Commands.Create;

internal sealed class TransferPaymentCommandHandler(IPaymentRepository paymentRepository, IBillRepository billRepository, IValidator validator)
: ICommandHandler<TransferPaymentCommand>
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IBillRepository _billRepository = billRepository;
    private readonly IValidator _validator = validator;

    public async Task<IResult> Handle(TransferPaymentCommand command, CancellationToken cancellationToken)
    {
        var billId = BillId.Create(command.from.billId);
        var bill = await _billRepository.GetByIdAsync(billId);
        _validator
            .If(bill is null, Error.NotFound<Bill>(billId));
        if (_validator.IsInvalid)
            return _validator.Failure();

        var result = await bill.TransferPayment(command.from.price, BillId.Create(command.to.billId), _billRepository);

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