using TravelAgency.Application.Abstractions.CQRS;

namespace TravelAgency.Application.Features.Users.Commands.ConfigureTwoFactorToptLogin;

public sealed record ConfigureTwoFactorToptLoginCommand : ICommand<TwoFactorToptResponse>;