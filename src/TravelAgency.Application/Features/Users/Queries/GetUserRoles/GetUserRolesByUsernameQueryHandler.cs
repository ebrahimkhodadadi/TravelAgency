using TravelAgency.Domain.Common.Errors;
using TravelAgency.Application.Abstractions;
using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Application.Mappings;
using TravelAgency.Application.Utilities;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Common.Results.Abstractions;
using TravelAgency.Domain.Users;
using TravelAgency.Domain.Users.ValueObjects;

namespace TravelAgency.Application.Features.Users.Queries.GetUserRoles;

internal sealed class GetUserRolesByUsernameQueryHandler(IUserRepository userRepository, IValidator validator)
    : IQueryHandler<GetUserRolesByUsernameQuery, RolesResponse>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IValidator _validator = validator;

    public async Task<IResult<RolesResponse>> Handle(GetUserRolesByUsernameQuery query, CancellationToken cancellationToken)
    {
        var usernameResult = Username.Create(query.Username);

        _validator
            .Validate(usernameResult);

        if (_validator.IsInvalid)
        {
            return _validator.Failure<RolesResponse>();
        }

        var user = await _userRepository
            .GetByUsernameAsync(usernameResult.Value, cancellationToken);

        if (user is null)
        {
            return Result.Failure<RolesResponse>(Error.NotFound<User>(query.Username));
        }

        return user
            .Roles
            .ToResponse()
            .ToResult();
    }
}
