using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Billing.Repositories;

public interface IPaymentRepository : IRepository
{
    void Add(Payment payment);
}
