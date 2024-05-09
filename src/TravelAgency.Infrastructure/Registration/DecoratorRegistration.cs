using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Infrastructure.Decoratos;

namespace TravelAgency.Infrastructure.Registration;

internal static class DecoratorRegistration
{
    internal static IServiceCollection RegisterDecorators(this IServiceCollection services)
    {
        //services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandlerDecorator<>));

        return services;
    }
}