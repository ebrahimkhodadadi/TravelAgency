using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Domain.Billing.Enumerations;

namespace TravelAgency.Application.Features.Payments.Commands.Create;

public sealed record TransferPaymentCommand(
    TransferPaymentFrom from,
    TransferPaymentTo to
    ) : ICommand;

public sealed record TransferPaymentFrom(
    int price,
    Ulid billId
    );

public sealed record TransferPaymentTo(
    Ulid billId
    );