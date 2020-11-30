using RestaurantOrderRouting.Domain.Aggregates.Orders.Queues;
using RestaurantOrderRouting.Infrastructure.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantOrderRouting.Domain.Aggregates.Orders
{
    /// <summary>
    /// Contract that defines methods for writing to OrderQueues;
    /// </summary>
    public interface IOrderWriter
    {
        Task<Order> Add(Order order);
    }

    /// <summary>
    /// Service implementation that writes to OrderQueues;
    /// </summary>
    internal sealed class OrderWriter : IOrderWriter
    {
        private readonly IReadOnlyCollection<IOrderQueue> _orderQueues;

        public OrderWriter(IReadOnlyCollection<IOrderQueue> orderQueues)
        {
            _orderQueues = orderQueues;
        }

        public async Task<Order> Add(Order order)
        {
            var queue = _orderQueues.GetQueue(order.KitchenArea);

            Logger.Log($"Order will be enqueued at {queue.KitchenArea}.", LogSeverity.Info);

            return await queue.Enqueue(order);
        }
    }
}
