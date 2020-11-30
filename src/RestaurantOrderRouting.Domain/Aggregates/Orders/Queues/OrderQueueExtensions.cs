using RestaurantOrderRouting.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantOrderRouting.Domain.Aggregates.Orders.Queues
{
    /// <summary>
    /// Extension methods for OrderQueue data structures.
    /// </summary>
    internal static class OrderQueueExtensions
    {
        public static IOrderQueue GetQueue(
            this IReadOnlyCollection<IOrderQueue> queues, KitchenArea kitchenArea)
        {
            var queue = queues.SingleOrDefault(x => x.KitchenArea == kitchenArea);

            return queue ?? throw new Exception($"Could not find a queue to {kitchenArea} area.");
        }
    }
}
