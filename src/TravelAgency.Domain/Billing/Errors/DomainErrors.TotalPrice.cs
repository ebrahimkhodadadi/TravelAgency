using TravelAgency.Domain.Common.Errors;

namespace TravelAgency.Domain.Billing.Errors
{
    public static partial class DomainErrors
    {
        public static class TotalPrice
        {
            public static readonly Error SameTotalPrice = 
                Error.New($"{nameof(TotalPrice)}.{nameof(TotalPrice)}", "جمع سفر ها و فاکتور ها برابر نیست");
        }
    }
}
