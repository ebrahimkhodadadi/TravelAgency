using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Billing.Repositories;
using TravelAgency.Domain.Common.Errors;
using TravelAgency.Domain.Common.Results;
using static TravelAgency.Domain.Billing.Enumerations.PaymentType;
namespace TravelAgency.Application.Features.Payments.Commands.Create;

internal sealed class DeletePaymentCommandHandler(IPaymentRepository paymentRepository, IValidator validator)
: ICommandHandler<DeletePaymentCommand>
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IValidator _validator = validator;

    public async Task<IResult> Handle(DeletePaymentCommand command, CancellationToken cancellationToken)
    {
        var paymentId = PaymentId.Create(command.paymentId);
        var payment = await _paymentRepository.GetByIdAsync(paymentId);
        _validator
            .If(payment is null, Error.NotFound<Payment>(paymentId));
        if (_validator.IsInvalid)
            return _validator.Failure();

        if (payment.PaymentType is Transfer)
        {
            var transferPayment = await _paymentRepository.GetByIdAsync(payment.TransferId.Value);
            _paymentRepository.Remove(transferPayment);
        }

        _paymentRepository.Remove(payment);

        return Result.Success();
    }
}