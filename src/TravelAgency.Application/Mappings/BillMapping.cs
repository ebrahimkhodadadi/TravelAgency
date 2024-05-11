using TravelAgency.Application.Features.Bills.Commands.Create;
using TravelAgency.Domain.Billing;

namespace TravelAgency.Application.Mappings;

public static class TravelMapping
{
    public static CreateTravelResponse ToCreateResponse(this Travel travel)
    {
        return new CreateTravelResponse(travel.Id.Value);
    }
}
