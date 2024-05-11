using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Billing.Repositories;

public interface IBillRepository : IRepository
{
    Task<Bill> GetByIdAsync(BillId id, CancellationToken cancellationToken = default);
}
