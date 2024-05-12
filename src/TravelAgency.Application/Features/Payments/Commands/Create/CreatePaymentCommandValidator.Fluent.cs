using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Application.Features.Payments.Commands.Create
{
    internal class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.price).Must(x => x > 0).WithMessage("مبلغ پرداختی باید بیشتر از صفر باشد");
        }
    }
}
