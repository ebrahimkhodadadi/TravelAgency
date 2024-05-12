using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Billing.Repositories;
using TravelAgency.Persistence.Framework;
using TravelAgency.Persistence.Specifications.Bills;
using TravelAgency.Persistence.Specifications;

namespace TravelAgency.Persistence.Repositories;

public sealed class PaymentRepository(TravelAgencyDbContext dbContext) : IPaymentRepository
{
    private readonly TravelAgencyDbContext _dbContext = dbContext;

    public async Task<Payment> GetByIdAsync(PaymentId id, CancellationToken cancellationToken)
    {
        var specification = PaymentSpecification.ById.Create(id);

        return await _dbContext
            .Set<Payment>()
            .UseSpecification(specification)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public void Add(Payment payment) =>
    _dbContext
        .Set<Payment>()
        .Add(payment);

    public void Remove(Payment payment) =>
        _dbContext
        .Set<Payment>()
        .Remove(payment);
}
