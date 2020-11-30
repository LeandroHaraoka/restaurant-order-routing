using Moq;
using RestaurantOrderRouting.Domain.Aggregates.Orders.Queues;
using System;
using System.Collections.Generic;

namespace RestaurantOrderRouting.Tests.Unit.Domain.Aggregates.Orders.Factories
{
    /// <summary>
    /// Factory for creating OrderQueue services
    /// </summary>
    internal sealed class OrderQueueFactory
    {
        /// <summary>
        /// Create a collection with instances of all IOrderQueue implementation.
        /// </summary>
        internal static IEnumerable<IOrderQueue> CreateOrderQueues()
        {
            var orderQueueTypes = IOrderQueueImplementations.GetAllTypes();

            foreach (var type in orderQueueTypes)
            {
                yield return (IOrderQueue)Activator.CreateInstance(type);
            }
        }
    }
}
