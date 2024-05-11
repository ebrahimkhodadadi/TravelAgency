using TravelAgency.Domain.Billing.ValueObjects;
using TravelAgency.Domain.Users;
using TravelAgency.Domain.Users.Enumerations;

namespace TravelAgency.Domain.Billing.Services;

public static class CreditService
{
    // هزینه استفاده از اعتبار
    public static Money CalculateCreditUsageFee(Rank customerRank, Money averageDailyBalance)
    {
        if (customerRank != Rank.Credit)
            return 0;

        Money creditUsageFee = 0;
        if (averageDailyBalance > 0)
        {
            creditUsageFee = (int)Math.Round(averageDailyBalance.Value * 0.02m);
        }

        return creditUsageFee;
    }
}