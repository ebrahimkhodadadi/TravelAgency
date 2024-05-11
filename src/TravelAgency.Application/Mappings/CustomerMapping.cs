using TravelAgency.Application.Features.Customers.Commands.Create;
using TravelAgency.Domain.Users;

namespace TravelAgency.Application.Mappings;

public static class CustomerMapping
{
    public static CreateCustomerResponse ToCreateResponse(this Customer customer)
    {
        return new CreateCustomerResponse(customer.Id.Value);
    }
}
