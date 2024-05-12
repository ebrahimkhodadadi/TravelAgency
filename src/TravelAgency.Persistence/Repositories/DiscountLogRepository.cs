using TravelAgency.Domain.Billing.Repositories;
using TravelAgency.Persistence.Framework;

namespace TravelAgency.Persistence.Repositories;

public sealed class DiscountLogRepository(TravelAgencyDbContext dbContext) : IDiscountLogRepository
{
    private readonly TravelAgencyDbContext _dbContext = dbContext;
}
