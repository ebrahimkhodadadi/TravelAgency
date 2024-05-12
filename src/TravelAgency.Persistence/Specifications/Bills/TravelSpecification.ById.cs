using TravelAgency.Domain.Billing;

namespace TravelAgency.Persistence.Specifications.Bills;

internal class TravelSpecification
{
    internal static partial class ById
    {
        internal static Specification<Travel, TravelId> Create(TravelId travelId)
        {
            var specification = Specification<Travel, TravelId>.New()
                .AddFilters(travel => travel.Id == travelId);

            return specification;
        }
    }
}