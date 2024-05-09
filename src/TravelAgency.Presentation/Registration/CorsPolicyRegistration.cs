using Microsoft.AspNetCore.Builder;

namespace TravelAgency.Presentation.Registration;

internal static class CorsPolicyRegistration
{
    public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
    {
        return app.UseCors(corsPolicyBuilder => corsPolicyBuilder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
    }
}