using TravelAgency.Application.Abstractions.CQRS;

namespace TravelAgency.Application.Features.Payments.Commands.Create;

public sealed record DeletePaymentCommand(
    Ulid paymentId
    ) : ICommand;
