using static TravelAgency.Domain.Billing.Enumerations.BillStatus;
using static TravelAgency.Domain.Billing.Constants.Bill;

namespace TravelAgency.Domain.Billing.Enumerations;

public static class BillStatusUtilities
{
    public static bool CanBeChangedTo(this BillStatus source, BillStatus destination)
    {
        return AvailableBillStatusChangeCombinations.Contains((source, destination));
    }
}