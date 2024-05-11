using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Billing.Repositories;
using TravelAgency.Persistence.Framework;

namespace TravelAgency.Persistence.Repositories;

public sealed class TravelRepository(TravelAgencyDbContext dbContext) : ITravelRepository
{
    private readonly TravelAgencyDbContext _dbContext = dbContext;

    public void Add(Travel travel) =>
        _dbContext
        .Set<Travel>()
        .Add(travel);
}
