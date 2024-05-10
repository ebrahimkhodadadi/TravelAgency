using TravelAgency.Domain.Billing.ValueObjects;
using TravelAgency.Domain.Common.Errors;

namespace TravelAgency.Domain.Billing.Errors
{
    public static partial class DomainErrors
    {
        public static class Price
        {
            public static readonly Error SameTotalPrice = 
                Error.New($"{nameof(Price)}.{nameof(Price)}", "جمع سفر ها و فاکتور ها برابر نیست");

            public static Error MustPayBeforeCreateTravel(Money price)
            {
                return Error.New($"{nameof(BillStatus)}.{nameof(MustPayBeforeCreateTravel)}",
                    $"برای ثبت سفر ابتدا مبلغ سفر را پرداخت نمایید {price.ToString()}");
            }            
            
            public static readonly Error MustUseLessThanCreditLimit = 
                Error.New($"{nameof(BillStatus)}.{nameof(MustUseLessThanCreditLimit)}",$"سفف استفاده از اعتبار به اتمام رسیده است");

            public static readonly Error MustHaveLessThanSumCheque = 
                Error.New($"{nameof(BillStatus)}.{nameof(MustHaveLessThanSumCheque)}",
                    "مجموع پرداخت های اسنادی باید کمتر از 50 درصد کل مبلغ فاکتور باشد");           
            
            public static readonly Error MustNotEquelForCheque = 
                Error.New($"{nameof(BillStatus)}.{nameof(MustNotEquelForCheque)}",
                    "در پرداخت به صورت تهاتر یکی از مبلغ ها باید منفی مبلغ دیگر باشد");

        }
    }
}
