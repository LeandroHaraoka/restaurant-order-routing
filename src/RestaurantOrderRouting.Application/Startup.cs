using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using RestaurantOrderRouting.Application.Middlewares;
using RestaurantOrderRouting.Application.Setups;
using RestaurantOrderRouting.Domain;
using RestaurantOrderRouting.Services;

namespace RestaurantOrderRouting.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddFluentValidation()
                .AddNewtonsoftJson((options) =>
                    options.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services
                .AddDocumentation()
                .AddDomain()
                .AddServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseDocumentation(Configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
