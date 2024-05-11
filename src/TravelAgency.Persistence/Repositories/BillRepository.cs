using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Billing.Repositories;
using TravelAgency.Domain.Users;
using TravelAgency.Persistence.Framework;
using TravelAgency.Persistence.Specifications;
using TravelAgency.Persistence.Specifications.Bills;

namespace TravelAgency.Persistence.Repositories;

public sealed class BillRepository(TravelAgencyDbContext dbContext) : IBillRepository
{
    private readonly TravelAgencyDbContext _dbContext = dbContext;

    public async Task<Bill> GetByIdAsync(BillId id, CancellationToken cancellationToken)
    {
        var specification = BillSpecification.ById.WithPayments.AndTravels.Create(id);

        return await _dbContext
            .Set<Bill>()
            .UseSpecification(specification)
            .FirstAsync(cancellationToken);
    }

    public void Add(Bill bill) =>
    _dbContext
        .Set<Bill>()
        .Add(bill);
}