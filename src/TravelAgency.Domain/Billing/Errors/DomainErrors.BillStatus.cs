using TravelAgency.Domain.Billing.Enumerations;
using TravelAgency.Domain.Common.Errors;

namespace TravelAgency.Domain.Billing.Errors;

public static partial class DomainErrors
{
    public static class BillStatus
    {
        /// <summary>
        /// Create an Error based on the bill status change
        /// </summary>
        /// <returns>InvalidStatusChange error</returns>
        public static Error InvalidStatusChange(Enumerations.BillStatus currentStatus, Enumerations.BillStatus destinationStatus)
        {
            return Error.New($"{nameof(BillStatus)}.{nameof(InvalidStatusChange)}", $"Cannot change status from {currentStatus} to {destinationStatus}.");
        }

        public static readonly Error BillClosed = Error.New($"{nameof(BillStatus)}.{nameof(BillClosed)}", $"Bill Closed.");
    }
}