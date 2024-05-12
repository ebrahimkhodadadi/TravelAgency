using TravelAgency.Domain.Billing.ValueObjects;

namespace TravelAgency.Domain.Billing.Services
{
    public static class DiscountService
    {
        // تخفیف خوش حسابی
        public static Money CalculateGoodPayerDiscount(Dictionary<DateTime, Money> purchaseList, Money totalPurchases)
        {
            if (purchaseList.Count < 30)
                return 0;

            bool isGoodPayer = IsGoodPayer(purchaseList);

            Money goodPayerDiscount = 0;
            if (isGoodPayer)
            {
                goodPayerDiscount = (int)Math.Round(totalPurchases.Value * 0.01m);
            }

            return goodPayerDiscount;
        }

        private static bool IsGoodPayer(Dictionary<DateTime, Money> purchaseList)
        {
            foreach (var avg in purchaseList)
            {
                if (avg.Value.Value < 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
