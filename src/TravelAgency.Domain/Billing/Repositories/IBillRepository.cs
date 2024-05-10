namespace TravelAgency.Domain.Billing.Repositories;

public interface IBillRepository
{
    Task<Bill> GetByIdAsync(BillId id, CancellationToken cancellationToken = default);
}
