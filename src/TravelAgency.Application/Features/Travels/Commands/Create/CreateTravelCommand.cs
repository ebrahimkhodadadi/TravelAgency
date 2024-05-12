using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Domain.Billing.Enumerations;
using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Billing.ValueObjects;
using TravelAgency.Application.Features.Payments.Commands.Create;

namespace TravelAgency.Application.Features.Travels.Commands.Create
{
    public sealed record CancelTravelCommand(
        Direction direction,
        DateTimeOffset start,
        TravelType type,
        int price,
        Ulid billId,
        CreatePaymentCommand? payment
        ) : ICommand<CancelTravelResponse>;
}
