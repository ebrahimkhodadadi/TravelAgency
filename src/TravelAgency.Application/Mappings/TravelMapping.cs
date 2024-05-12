using TravelAgency.Application.Features.Customers.Commands.Create;
using TravelAgency.Application.Features.Travels.Commands.Create;
using TravelAgency.Domain.Billing;
using TravelAgency.Domain.Users;

namespace TravelAgency.Application.Mappings;

public static class TravelMapping
{
    public static CancelTravelResponse ToCreateResponse(this Travel travel)
    {
        return new CancelTravelResponse(travel.Id.Value);
    }
}