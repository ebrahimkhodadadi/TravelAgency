using FluentValidation;
using TravelAgency.Application.Features.Users.Queries.GetUserByUsername;

namespace TravelAgency.Application.Features.Users.Queries.GetUserRoles;

internal sealed class GetUserRolesByUsernameQueryValidator : AbstractValidator<GetUserByUsernameQuery>
{
    public GetUserRolesByUsernameQueryValidator()
    {
        RuleFor(x => x.Username).NotNull();
    }
}