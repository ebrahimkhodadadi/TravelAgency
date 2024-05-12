using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Billing.Repositories;
using TravelAgency.Persistence.Framework;
using TravelAgency.Persistence.Specifications;
using TravelAgency.Persistence.Specifications.Bills;

namespace TravelAgency.Persistence.Repositories;

public sealed class TravelRepository(TravelAgencyDbContext dbContext) : ITravelRepository
{
    private readonly TravelAgencyDbContext _dbContext = dbContext;

    public async Task<Travel> GetByIdAsync(TravelId id, CancellationToken cancellationToken)
    {
        var specification = TravelSpecification.ById.Create(id);

        return await _dbContext
            .Set<Travel>()
            .UseSpecification(specification)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public void Add(Travel travel) =>
        _dbContext
        .Set<Travel>()
        .Add(travel);
}
