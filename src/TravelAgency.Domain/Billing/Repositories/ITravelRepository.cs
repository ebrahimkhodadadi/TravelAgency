using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Billing.Repositories
{
    public interface ITravelRepository : IRepository
    {
        Task<Travel> GetByIdAsync(TravelId id, CancellationToken cancellationToken = default);

        void Add(Travel bill);
    }
}
