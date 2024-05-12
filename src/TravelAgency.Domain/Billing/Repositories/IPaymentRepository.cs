using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Billing.Repositories;

public interface IPaymentRepository : IRepository
{
    Task<Payment> GetByIdAsync(PaymentId id, CancellationToken cancellationToken = default);

    void Add(Payment payment);
}
