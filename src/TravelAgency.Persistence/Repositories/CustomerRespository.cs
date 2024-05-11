using Microsoft.EntityFrameworkCore;
using System.Threading;
using TravelAgency.Domain.Users;
using TravelAgency.Domain.Users.ValueObjects;
using TravelAgency.Persistence.Framework;

namespace TravelAgency.Persistence.Repositories;


public sealed class CustomerRespository(TravelAgencyDbContext dbContext) : ICustomerRepository
{
    private readonly TravelAgencyDbContext _dbContext = dbContext;

    public void Add(Customer customer) =>
        _dbContext
            .Set<Customer>()
            .Add(customer);

    public async Task<bool> IsPhoneNumberTakenAsync(PhoneNumber phoneNumber, CancellationToken ct) =>
        await _dbContext
            .Set<Customer>()
            .Where(customer => customer.PhoneNumber == phoneNumber)
            .AnyAsync(ct);
}