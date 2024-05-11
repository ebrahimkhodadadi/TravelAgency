using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Application.Pipelines;
using TravelAgency.Persistence.Pipelines;

namespace TravelAgency.Persistence.Registration;

internal static class MediatorRegistration
{
    internal static IServiceCollection RegisterMediator(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(TravelAgency.Application.AssemblyReference.Assembly);
            configuration.AddOpenBehavior(typeof(LoggingPipeline<,>));
            configuration.AddOpenBehavior(typeof(FluentValidationPipeline<,>));
            configuration.AddOpenBehavior(typeof(QueryTransactionPipeline<,>));
            configuration.AddOpenBehavior(typeof(CommandTransactionPipeline<,>));
            configuration.AddOpenBehavior(typeof(CommandWithResponseTransactionPipeline<,>));
            configuration.AddOpenBehavior(typeof(ReferenceValidationPipeline<,>));
            configuration.AddOpenBehavior(typeof(QueryCachingPipeline<,>));
        });

        return services;
    }
}
