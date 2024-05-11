using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Domain.Users.Enumerations;

namespace TravelAgency.Application.Features.Customers.Commands.Create;

public sealed record CreateCustomerCommand(
        string firstName,
        string lastName,
        //string email,
        Gender gender,
        string contactNumber,   
        Rank rank,
        int? debtLimit
    ) : ICommand<CreateCustomerResponse>;
