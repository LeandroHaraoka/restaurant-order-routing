using Microsoft.Extensions.DependencyInjection;
using RestaurantOrderRouting.Domain.Aggregates.Orders;
using RestaurantOrderRouting.Domain.Aggregates.Orders.Queues;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RestaurantOrderRouting.Tests.Unit")]
namespace RestaurantOrderRouting.Domain
{
    /// <summary>
    /// Setup of dependecies located at domain layer.
    /// </summary>
    public static class Setup
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddOrderQueues();
            services.AddScoped<IReadOnlyCollection<IOrderQueue>>(p => p.GetServices<IOrderQueue>().ToList());
            return services
                .AddScoped<IOrderWriter, OrderWriter>()
                .AddScoped<IOrderReader, OrderReader>();
        }

        private static IServiceCollection AddOrderQueues(this IServiceCollection services)
        {
            var types = IOrderQueueImplementations.GetAllTypes();
            foreach (var type in types)
                services.AddSingleton(typeof(IOrderQueue), type);
            return services;
        }
    }
}
