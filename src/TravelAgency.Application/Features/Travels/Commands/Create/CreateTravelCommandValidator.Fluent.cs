using FluentValidation;

namespace TravelAgency.Application.Features.Travels.Commands.Create
{
    internal class CreateTravelCommandValidator : AbstractValidator<CancelTravelCommand>
    {
        public CreateTravelCommandValidator()
        {
            RuleFor(x => x.price).Must(x => x > 0).WithMessage("مبلغ سفر باید بیشتر از صفر باشد");
        }
    }
}
