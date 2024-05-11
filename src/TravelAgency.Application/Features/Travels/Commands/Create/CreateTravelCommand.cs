using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Domain.Billing.Enumerations;
using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Billing.ValueObjects;

namespace TravelAgency.Application.Features.Bills.Commands.Create
{
    public sealed record CreateTravelCommand(
        Direction direction,
        DateTimeOffset start,
        TravelType type,
        int price,
        Ulid billId
        ) : ICommand<CreateTravelResponse>;
}
