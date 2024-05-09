using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgency.Domain.Users.ValueObjects;

namespace TravelAgency.Persistence.Converters.ValueObjects;

public sealed class EmailConverter : ValueConverter<Email, string>
{
    public EmailConverter() : base(email => email.Value, @string => Email.Create(@string).Value) { }
}

public sealed class EmailComparer : ValueComparer<Email>
{
    public EmailComparer() : base((email1, email2) => email1!.Value == email2!.Value, email => email.GetHashCode()) { }
}