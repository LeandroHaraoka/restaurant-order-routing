using RestaurantOrderRouting.Domain.Aggregates.Orders.Queues;
using RestaurantOrderRouting.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantOrderRouting.Domain.Aggregates.Orders
{
    /// <summary>
    /// Contract that defines methods for reading OrderQueues;
    /// </summary>
    public interface IOrderReader
    {
        Task<Order> Dequeue(KitchenArea kitchenArea);
    }

    /// <summary>
    /// Service implementation that reads OrderQueues;
    /// </summary>
    internal sealed class OrderReader : IOrderReader
    {
        private readonly IReadOnlyCollection<IOrderQueue> _orderQueues;

        public OrderReader(IReadOnlyCollection<IOrderQueue> orderQueues) => _orderQueues = orderQueues;

        public async Task<Order> Dequeue(KitchenArea kitchenArea)
        {
            var queue = _orderQueues.GetQueue(kitchenArea);
            return await queue.Dequeue();
        }
    }
}
