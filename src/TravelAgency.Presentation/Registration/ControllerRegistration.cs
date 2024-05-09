using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using TravelAgency.Presentation.Resolvers;
using TravelAgency.Presentation;
using static Newtonsoft.Json.Formatting;
using static Newtonsoft.Json.ReferenceLoopHandling;
using ApiBehaviorOptions = TravelAgency.Presentation.Options.ApiBehaviorOptions;

namespace TravelAgency.Presentation.Registration;

internal static class ControllerRegistration
{
    internal static IServiceCollection RegisterControllers(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddApplicationPart(AssemblyReference.Assembly)
            .ConfigureApiBehaviorOptions(options =>
                options.InvalidModelStateResponseFactory = ApiBehaviorOptions.InvalidModelStateResponse)
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new RequiredPropertiesCamelCaseContractResolver();
                options.SerializerSettings.Formatting = Indented;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.ReferenceLoopHandling = Ignore;
            });

        return services;
    }
}
