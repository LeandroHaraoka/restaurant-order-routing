using Microsoft.Extensions.DependencyInjection;
using RestaurantOrderRouting.Services.Handlers;
using RestaurantOrderRouting.Services.Messages;
using FluentValidation;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RestaurantOrderRouting.Tests.Unit")]
namespace RestaurantOrderRouting.Services
{
    /// <summary>
    /// Setup of dependecies located at services layer.
    /// </summary>
    public static class Setup
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddValidators();
            return services
                .AddScoped<ICreateOrderHandler, CreateOrderHandler>()
                .AddScoped<IRemoveOrderHandler, RemoveOrderHandler>();
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            return services.AddScoped<IValidator<CreateOrderRequest>, CreateOrderRequestValidator>();
        }
    }
}
