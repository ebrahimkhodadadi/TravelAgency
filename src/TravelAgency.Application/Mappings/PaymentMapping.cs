using TravelAgency.Application.Features.Payments.Commands.Create;
using TravelAgency.Domain.Billing;

namespace TravelAgency.Application.Mappings;
public static class PaymentMapping
{
    public static CreatePaymentResponse ToCreateResponse(this Payment payment)
    {
        return new CreatePaymentResponse(payment.Id.Value);
    }
}
