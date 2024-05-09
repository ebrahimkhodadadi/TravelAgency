using FluentValidation;

namespace TravelAgency.Application.Features.Users.Queries.GetUserByUsername;

internal sealed class GetUserByUsernameQueryValidator : AbstractValidator<GetUserByUsernameQuery>
{
    public GetUserByUsernameQueryValidator()
    {
        RuleFor(x => x.Username).NotNull();
    }
}