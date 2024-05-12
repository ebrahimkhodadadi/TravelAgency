using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Billing.Repositories;
using TravelAgency.Persistence.Framework;

namespace TravelAgency.Persistence.Repositories;

public sealed class PaymentRepository(TravelAgencyDbContext dbContext) : IPaymentRepository
{
    private readonly TravelAgencyDbContext _dbContext = dbContext;

    public void Add(Payment payment) =>
    _dbContext
        .Set<Payment>()
        .Add(payment);
}
