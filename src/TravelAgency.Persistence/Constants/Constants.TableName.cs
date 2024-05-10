namespace TravelAgency.Persistence.Constants;

public static partial class Constants
{
    internal static class TableName
    {
        internal const string Address = nameof(Address);
        internal const string Bill = nameof(Bill);
        internal const string Customer = nameof(Customer);
        internal const string Payment = nameof(Payment);
        internal const string Travel = nameof(Travel);
        internal const string Review = nameof(Review);
        internal const string Role = nameof(Role);
        internal const string RoleUser = nameof(RoleUser);
        internal const string Permission = nameof(Permission);
        internal const string RolePermission = nameof(RolePermission);
        internal const string Tag = nameof(Tag);
        internal const string User = nameof(User);
        internal const string OutboxMessage = nameof(OutboxMessage);
        internal const string OutboxMessageConsumer = nameof(OutboxMessageConsumer);
    }
}