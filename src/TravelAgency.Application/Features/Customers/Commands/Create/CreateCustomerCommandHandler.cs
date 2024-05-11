using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Domain.Common.Results.Abstractions;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Users;
using TravelAgency.Domain.Users.ValueObjects;
using TravelAgency.Application.Mappings;
using TravelAgency.Application.Utilities;
using TravelAgency.Application.Abstractions;
using static TravelAgency.Domain.Users.Errors.DomainErrors;

namespace TravelAgency.Application.Features.Customers.Commands.Create
{
    internal sealed class CreateCustomerCommandHandler(ICustomerRepository customerRepository, IValidator validator)
    : ICommandHandler<CreateCustomerCommand, CreateCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IValidator _validator = validator;

        public async Task<IResult<CreateCustomerResponse>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var firstNameResult = FirstName.Create(command.firstName);
            var lastNameResult = LastName.Create(command.lastName);
            //var emailResult = Email.Create(command.email);
            var phonenumberResult = PhoneNumber.Create(command.contactNumber);
            _validator
                .Validate(firstNameResult)
                .Validate(lastNameResult)
                //.Validate(emailResult)
                .Validate(phonenumberResult);
            if (_validator.IsInvalid)
            {
                return await _validator
                    .Failure<CreateCustomerResponse>()
                    .ToResult()
                    .ToTask();
            }
            bool phoneIsTaken = await _customerRepository
                .IsPhoneNumberTakenAsync(phonenumberResult.Value, cancellationToken);
            _validator
                .If(phoneIsTaken, thenError: PhoneNumberError.PhoneNumberAlreadyTaken);
            if (_validator.IsInvalid)
            {
                return await _validator
                    .Failure<CreateCustomerResponse>()
                    .ToResult()
                    .ToTask();
            }

            //var user = User.Create(UserId.New(), usernameResult.Value, emailResult.Value);
            var customer = Customer.Create
            (
                id: CustomerId.New(),
                firstNameResult.Value,
                lastNameResult.Value,
                null,//emailResult.Value,
                command.gender,
                phonenumberResult.Value,
                null,
                //user,
                command.rank,
                DebtLimit.Create(command.debtLimit.HasValue ? command.debtLimit.Value : 0).Value
            );

            _customerRepository.Add(customer);

            return await customer
                .ToCreateResponse()
                .ToResult()
                .ToTask();
        }
    }
}
