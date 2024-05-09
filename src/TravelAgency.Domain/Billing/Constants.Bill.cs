using TravelAgency.Domain.Billing.Enumerations;
using static TravelAgency.Domain.Billing.Enumerations.BillStatus;

namespace TravelAgency.Domain.Billing;

public static partial class Constants
{
    public static class Bill
    {
        public readonly static List<(BillStatus source, BillStatus destination)> AvailableBillStatusChangeCombinations =
        [
            (InProgress, Canceled),
            (InProgress, Closed),
        ];
    }
}