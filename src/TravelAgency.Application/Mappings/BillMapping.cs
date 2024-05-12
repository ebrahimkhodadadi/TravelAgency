using TravelAgency.Application.Features.Bills.Commands.Create;
using TravelAgency.Domain.Billing;

namespace TravelAgency.Application.Mappings;

public static class BillMapping
{
    public static CreateBillResponse ToCreateResponse(this Bill bill)
    {
        return new CreateBillResponse(bill.Id.Value);
    }
}
