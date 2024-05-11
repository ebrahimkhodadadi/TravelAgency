using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Domain.Billing.Repositories
{
    public interface ITravelRepository : IRepository
    {
        void Add(Travel bill);
    }
}
