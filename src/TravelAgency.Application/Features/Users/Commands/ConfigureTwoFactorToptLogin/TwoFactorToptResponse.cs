using TravelAgency.Application.Abstractions;

namespace TravelAgency.Application.Features.Users.Commands.ConfigureTwoFactorToptLogin;

public sealed record TwoFactorToptResponse
(
    string QrCode
) : IResponse;
