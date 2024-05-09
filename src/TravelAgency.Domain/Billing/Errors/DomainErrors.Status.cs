using TravelAgency.Domain.Billing.Enumerations;
using TravelAgency.Domain.Common.Errors;

namespace TravelAgency.Domain.Billing.Errors;

public static partial class DomainErrors
{
    public static class Status
    {
        /// <summary>
        /// Create an Error based on the bill status change
        /// </summary>
        /// <returns>InvalidStatusChange error</returns>
        public static Error InvalidStatusChange(BillStatus currentStatus, BillStatus destinationStatus)
        {
            return Error.New($"{nameof(Status)}.{nameof(InvalidStatusChange)}", $"Cannot change status from {currentStatus} to {destinationStatus}.");
        }

        public static readonly Error BillClosed = Error.New($"{nameof(Status)}.{nameof(BillClosed)}", $"Bill Closed.");
    }
}