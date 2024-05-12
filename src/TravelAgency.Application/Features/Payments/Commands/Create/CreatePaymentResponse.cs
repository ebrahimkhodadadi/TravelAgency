namespace TravelAgency.Application.Features.Payments.Commands.Create;

public sealed record CreatePaymentResponse
(
    Ulid Id
) : IResponse;