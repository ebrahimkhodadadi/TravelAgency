using TravelAgency.Application.Abstractions;
using TravelAgency.Domain.Common.DataProcessing.Abstractions;

namespace TravelAgency.Application.Features.Users.Queries.GetUserByUsername;

public sealed record UserResponse
(
    Ulid Id,
    string Username,
    string Email,
    Ulid? CustomerId
)
    : IResponse, IHasCursor;