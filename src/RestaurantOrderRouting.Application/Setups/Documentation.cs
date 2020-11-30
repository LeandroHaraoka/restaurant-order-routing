using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RestaurantOrderRouting.Application.Setups
{
    /// <summary>
    /// Setup of dependencies responsible for documentation resources, such as Swagger and Redocs.
    /// </summary>
    internal static class Documentation
    {
        internal static IServiceCollection AddDocumentation(this IServiceCollection services) =>
            services.AddOpenApiDocument((settings, provider) =>
            {
                settings.Title = "Restaurant Order Routing API";
                settings.Description = "Powered by .Net Core 3.1.";
                settings.GenerateXmlObjects = true;
            });

        internal static IApplicationBuilder UseDocumentation(
            this IApplicationBuilder builder,IConfiguration configuration) =>
                builder
                    .UseOpenApi()
                    .UseReDoc((settings) => settings.Path = configuration["Api:Routes:Redoc"])
                    .UseSwaggerUi3((settings) =>
                    {
                        settings.Path = configuration["Api:Routes:Swagger"];
                        settings.OperationsSorter = "method";
                        settings.TagsSorter = "alpha";
                    });
    }
}
