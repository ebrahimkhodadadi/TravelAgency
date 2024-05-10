using TravelAgency.Domain.Billing.ValueObjects;

namespace TravelAgency.Domain.Billing.Services
{
    public static class DiscountService
    {
        // تخفیف خوش حسابی
        public static Money CalculateGoodPayerDiscount(List<Payment> payments)
        {
            DateTime endDate = DateTimeOffset.UtcNow.Date;
            DateTime startDate = endDate.AddDays(-29);

            List<Payment> paymentsIn30Days = payments
                .Where(p => p.CreatedOn >= startDate && p.CreatedOn <= endDate)
                .ToList();

            bool isGoodPayer = IsGoodPayer(paymentsIn30Days, startDate, endDate);

            Money goodPayerDiscount = 0;
            if (isGoodPayer)
            {
                Money totalPurchases = CalculateTotalPurchases(paymentsIn30Days);
                goodPayerDiscount = (int)Math.Round(totalPurchases.Value * 0.01m);
            }

            return goodPayerDiscount;
        }

        private static bool IsGoodPayer(List<Payment> payments, DateTime startDate, DateTime endDate)
        {
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                Money averageDailyBalance = CreditService.CalculateAverageDailyBalance(date, date.AddDays(9), payments);

                if (averageDailyBalance >= 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static Money CalculateTotalPurchases(List<Payment> payments)
        {
            Money totalPurchases = payments.Sum(p => p.Price.Value);

            return totalPurchases;
        }
    }
}
