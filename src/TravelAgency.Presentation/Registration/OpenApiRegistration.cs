﻿using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using TravelAgency.Presentation;
using TravelAgency.Presentation.OpenApi;

namespace TravelAgency.Presentation.Registration;

internal static class OpenApiRegistration
{
    private const string SwaggerDarkThameStyleFileName = "SwaggerDark.css";
    private const string WwwRootDirectoryName = "OpenApi";
    private const string TravelAgencyPresentation = $"{nameof(TravelAgency)}.{nameof(TravelAgency.Presentation)}";

    internal static IServiceCollection RegisterOpenApi(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, OpenApiOptionsSetup>();

        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<OpenApiDefaultValues>();
            options.ExampleFilters();
            options.IncludeXmlDocumentation(AssemblyReference.Assembly);
            options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            options.OperationFilter<SecurityRequirementsOperationFilter>();
            options.AddJwtAuthorization();
            options.AddApiKeyAuthorization();
        });

        services.AddSwaggerExamplesFromAssemblies(AssemblyReference.Assembly);

        return services;
    }

    internal static IApplicationBuilder ConfigureOpenApi(this IApplicationBuilder app, bool isDevelopment)
    {
        if (isDevelopment)
        {
            var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();

            UseStaticFiles(app);

            app.UseSwaggerUI(options =>
            {
                foreach (var groupName in provider.ApiVersionDescriptions.Select(x => x.GroupName))
                {
                    options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
                }

                options.InjectStylesheet($"/{SwaggerDarkThameStyleFileName}");
            });
        }

        return app;
    }

    /// <summary>
    /// Used for the WebApplicationFactory (tests) reason.
    /// </summary>
    private static void UseStaticFiles(IApplicationBuilder app)
    {
        var wwwRoot = Path.Combine($"{Directory.GetParent(Directory.GetCurrentDirectory())}", TravelAgencyPresentation, WwwRootDirectoryName);

        try
        {
            app
                .UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(wwwRoot)
                });
        }
        catch
        {
            app
                .UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory())
                });
        }
    }
}
