using static TravelAgency.Domain.Billing.Enumerations.TravelStatus;

namespace TravelAgency.Domain.Billing.Enumerations;

public static partial class Constants
{
    public static class TravelStatusConst
    {
        public readonly static List<(TravelStatus source, TravelStatus destination)> AvailableTravelStatusChangeCombinations =
        [
            (InProgress, Canceled),
        ];
    }
}