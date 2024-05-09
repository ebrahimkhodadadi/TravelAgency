using TravelAgency.Domain.Common.Errors;
using TravelAgency.Application.Abstractions;
using TravelAgency.Application.Abstractions.CQRS;
using TravelAgency.Domain.Common.Results;
using TravelAgency.Domain.Common.Results.Abstractions;
using TravelAgency.Domain.Users;

namespace TravelAgency.Application.Features.Users.Commands.Revoke;

internal sealed class RevokeRefreshTokenCommandHandler
(
    IUserRepository userRepository,
    IValidator validator
)
    : ICommandHandler<RevokeRefreshTokenCommand>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IValidator _validator = validator;

    public async Task<IResult> Handle(RevokeRefreshTokenCommand command, CancellationToken cancellationToken)
    {
        User? user = await _userRepository
            .GetByIdAsync(command.UserId, cancellationToken);

        _validator
            .If(user is null, thenError: Error.InvalidReference(command.UserId.Value, nameof(User)));

        if (_validator.IsInvalid)
        {
            return _validator.Failure();
        }

        user!.RefreshToken = null;

        return Result.Success();
    }
}
