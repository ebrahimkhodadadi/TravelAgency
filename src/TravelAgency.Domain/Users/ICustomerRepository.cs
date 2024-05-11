using TravelAgency.Domain.Common.BaseTypes.Abstractions;
using TravelAgency.Domain.Users.ValueObjects;

namespace TravelAgency.Domain.Users;

public interface ICustomerRepository : IRepository
{
    Task<bool> IsPhoneNumberTakenAsync(PhoneNumber phoneNumber, CancellationToken ct);

    void Add(Customer customer);
}
