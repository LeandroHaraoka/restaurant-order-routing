using RestaurantOrderRouting.Domain.Aggregates.Orders.Queues;
using RestaurantOrderRouting.Domain.ValueObjects;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace RestaurantOrderRouting.Domain.Aggregates.Orders.Queuess
{
    /// <summary>
    /// KitchenAreaB implementation of IOrderQueue. Manages all orders that contain KitchenArea = KitchenB.
    /// </summary>
    public sealed class KitchenBOrderQueue : IOrderQueue
    {
        public KitchenArea KitchenArea => KitchenArea.KitchenB;
        private readonly ConcurrentQueue<Order> _kitchenBOrders;

        public KitchenBOrderQueue()
        {
            _kitchenBOrders = new ConcurrentQueue<Order>();
        }

        public async Task<Order> Enqueue(Order order)
        {
            _kitchenBOrders.Enqueue(order);
            return order;
        }

        public async Task<Order> Dequeue()
        {
            _kitchenBOrders.TryDequeue(out var order);
            return order;
        }
    }
}
