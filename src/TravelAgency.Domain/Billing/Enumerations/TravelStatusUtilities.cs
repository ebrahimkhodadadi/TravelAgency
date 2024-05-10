using static TravelAgency.Domain.Billing.Enumerations.TravelStatusUtilities;
using static TravelAgency.Domain.Billing.Enumerations.Constants.TravelStatusConst;

namespace TravelAgency.Domain.Billing.Enumerations;

public static class TravelStatusUtilities
{
    public static bool CanBeChangedTo(this TravelStatus source, TravelStatus destination)
    {
        return AvailableTravelStatusChangeCombinations.Contains((source, destination));
    }
}