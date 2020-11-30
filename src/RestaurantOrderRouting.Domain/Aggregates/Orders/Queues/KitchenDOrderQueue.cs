using RestaurantOrderRouting.Domain.Aggregates.Orders.Queues;
using RestaurantOrderRouting.Domain.ValueObjects;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace RestaurantOrderRouting.Domain.Aggregates.Orders.Queuess
{
    /// <summary>
    /// KitchenAreaD implementation of IOrderQueue. Manages all orders that contain KitchenArea = KitchenD.
    /// </summary>
    public sealed class KitchenDOrderQueue : IOrderQueue
    {
        public KitchenArea KitchenArea => KitchenArea.KitchenD;
        private readonly ConcurrentQueue<Order> _kitchenDOrders;

        public KitchenDOrderQueue()
        {
            _kitchenDOrders = new ConcurrentQueue<Order>();
        }

        public async Task<Order> Enqueue(Order order)
        {
            _kitchenDOrders.Enqueue(order);
            return order;
        }

        public async Task<Order> Dequeue()
        {
            _kitchenDOrders.TryDequeue(out var order);
            return order;
        }
    }
}
