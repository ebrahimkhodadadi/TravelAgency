using TravelAgency.Domain.Billing.ValueObjects;
using TravelAgency.Domain.Users;

namespace TravelAgency.Domain.Billing.Services;

public static class CreditService
{
    // هزینه استفاده از اعتبار
    public static Money CalculateCreditUsageFee(Customer customer, DateTime billStartDate, List<Payment> payments)
    {
        if (customer.Rank != Users.Enumerations.Rank.Credit)
            return 0;
        DateTime billEndDate = billStartDate.AddDays(10);
        if (DateTime.Now < billEndDate)
            return 0;

        // محاسبه صورت حساب روزانه
        Money averageDailyBalance = CalculateAverageDailyBalance(billStartDate, billEndDate, payments);

        Money creditUsageFee = 0;
        if (averageDailyBalance > 0)
        {
            creditUsageFee = (int)Math.Round(averageDailyBalance.Value * 0.02m);
        }

        return creditUsageFee;
    }

    // محاسبه بالانس روزانه
    internal static Money CalculateAverageDailyBalance(DateTime billStartDate, DateTime billEndDate, List<Payment> payments)
    {
        List<Payment> transactions = payments
            .Where(t => t.CreatedOn >= billStartDate && t.CreatedOn <= billEndDate)
            .OrderBy(t => t.CreatedOn)
            .ToList();

        Dictionary<DateTime, Money> dailyBalances = new Dictionary<DateTime, Money>();
        foreach (Payment transaction in transactions)
        {
            DateTime transactionDate = transaction.CreatedOn.DateTime;
            Money dailyBalance = transaction.Price;

            if (dailyBalances.ContainsKey(transactionDate))
            {
                dailyBalances[transactionDate] += dailyBalance;
            }
            else
            {
                dailyBalances[transactionDate] = dailyBalance;
            }
        }

        Money totalBalance = dailyBalances.Sum(kv => kv.Value.Value);
        int numberOfDays = (int)(billEndDate - billStartDate).TotalDays + 1;
        Money averageDailyBalance = totalBalance / numberOfDays;

        return averageDailyBalance;
    }
}