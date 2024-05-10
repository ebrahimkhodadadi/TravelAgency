using TravelAgency.Domain.Common.Errors;

namespace TravelAgency.Domain.Billing.Errors;

public static partial class DomainErrors
{
    public static class TravelStatus
    {
        /// <summary>
        /// Create an Error based on the travel status change
        /// </summary>
        /// <returns>InvalidStatusChange error</returns>
        public static Error InvalidStatusChange(Enumerations.TravelStatus currentStatus, Enumerations.TravelStatus destinationStatus)
        {
            return Error.New($"{nameof(BillStatus)}.{nameof(InvalidStatusChange)}", $"Cannot change status from {currentStatus} to {destinationStatus}.");
        }

        public static readonly Error CancelTrain = 
            Error.New($"{nameof(TravelStatus)}.{nameof(CancelTrain)}", $"پس از شروع سفر امکان کنسل کردن قطار نیست.");
    }
}