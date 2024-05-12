using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Domain.Billing.Enumerations;

namespace TravelAgency.Application.Features.Payments.Commands.Create;

public sealed record CreatePaymentCommand(
    int price,
    PaymentType paymentType,
    Ulid billId
    ) : ICommand<CreatePaymentResponse>;
