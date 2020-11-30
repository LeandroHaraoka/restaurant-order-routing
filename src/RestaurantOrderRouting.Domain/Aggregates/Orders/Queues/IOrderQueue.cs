using RestaurantOrderRouting.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace RestaurantOrderRouting.Domain.Aggregates.Orders.Queues
{
    /// <summary>
    /// All order queues must implement this interface, in order to manage kitchen orders.
    /// </summary>
    public interface IOrderQueue
    {
        KitchenArea KitchenArea { get; }
        Task<Order> Enqueue(Order order);
        Task<Order> Dequeue();
    }

    /// <summary>
    /// Small service that provides a collection containing all types that implement IOrderQueue.
    /// </summary>
    internal static class IOrderQueueImplementations
    {
        internal static IEnumerable<Type> GetAllTypes()
        {
            var @interface = typeof(IOrderQueue);
            var types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var type in types)
            {
                if (@interface.IsAssignableFrom(type) && !type.IsInterface)
                    yield return type;
            }
        }
    }
}
