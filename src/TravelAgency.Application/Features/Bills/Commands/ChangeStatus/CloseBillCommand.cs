using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Application.Features.Payments.Commands.Create;

namespace TravelAgency.Application.Features.Bills.Commands.ChangeStatus;

public sealed record CloseBillCommand(
    Ulid billId
    ) : ICommand;