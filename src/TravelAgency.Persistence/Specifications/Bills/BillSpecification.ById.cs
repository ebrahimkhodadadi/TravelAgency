using System.Collections.Frozen;
using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Common.DataProcessing;

namespace TravelAgency.Persistence.Specifications.Bills;

internal class BillSpecification
{
    internal static partial class ById
    {
        public static partial class WithPayments
        {
            public static partial class AndTravels
            {
                private static readonly FrozenSet<IncludeEntry<Bill>> _buildIncludesFrozen = IncludeBuilderOrchestrator<Bill>
                    .GetIncludeEntries(builder => builder
                        .Include(orderHeader => orderHeader.Customer)
                        .Include(orderHeader => orderHeader.Payments)
                        .Include(orderHeader => orderHeader.Travels)
                        .Include(orderHeader => orderHeader.DiscountLogs))
                    .ToFrozenSet();

                internal static Specification<Bill, BillId> Create(BillId billId)
                {
                    var specification = Specification<Bill, BillId>.New()
                        .AddIncludes(_buildIncludesFrozen)
                        .AddFilters(orderHeader => orderHeader.Id == billId);

                    return specification;
                }
            }
        }
    }
}