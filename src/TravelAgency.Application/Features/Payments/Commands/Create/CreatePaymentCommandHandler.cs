using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Application.Mappings;
using TravelAgency.Application.Utilities;
using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Billing.Repositories;
using TravelAgency.Domain.Common.Errors;

namespace TravelAgency.Application.Features.Payments.Commands.Create;

internal sealed class CreatePaymentCommandHandler(IPaymentRepository paymentRepository, IBillRepository billRepository, IValidator validator)
: ICommandHandler<CreatePaymentCommand, CreatePaymentResponse>
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IBillRepository _billRepository = billRepository;
    private readonly IValidator _validator = validator;

    public async Task<IResult<CreatePaymentResponse>> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        var billId = BillId.Create(command.billId);
        var bill = await _billRepository.GetByIdAsync(billId);
        _validator
            .If(bill is null, Error.NotFound<Bill>(billId));
        if (_validator.IsInvalid)
            return _validator.Failure<CreatePaymentResponse>();

        var paymentResult = bill.Pay(command.price, command.paymentType);

        _validator
            .Validate(paymentResult);
        if (_validator.IsInvalid)
        {
            return _validator
                .Failure<CreatePaymentResponse>()
                .ToResult();
        }

        return paymentResult.Value
            .ToCreateResponse()
            .ToResult();
    }
}