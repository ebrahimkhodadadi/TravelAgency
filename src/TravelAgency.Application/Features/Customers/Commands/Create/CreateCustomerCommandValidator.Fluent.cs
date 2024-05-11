using FluentValidation;

namespace TravelAgency.Application.Features.Customers.Commands.Create
{
    internal class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.firstName).NotNull();
            RuleFor(x => x.rank).NotNull();
        }
    }
}
