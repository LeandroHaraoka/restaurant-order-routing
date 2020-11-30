using RestaurantOrderRouting.Domain.Aggregates.Orders.Queues;
using RestaurantOrderRouting.Domain.ValueObjects;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace RestaurantOrderRouting.Domain.Aggregates.Orders.Queuess
{
    /// <summary>
    /// KitchenAreaA implementation of IOrderQueue. Manages all orders that contain KitchenArea = KitchenA.
    /// </summary>
    public sealed class KitchenAOrderQueue : IOrderQueue
    {
        public KitchenArea KitchenArea => KitchenArea.KitchenA;
        private readonly ConcurrentQueue<Order> _kitchenAOrders;

        public KitchenAOrderQueue()
        {
            _kitchenAOrders = new ConcurrentQueue<Order>();
        }

        public async Task<Order> Enqueue(Order order)
        {
            _kitchenAOrders.Enqueue(order);
            return order;
        }

        public async Task<Order> Dequeue()
        {
            _kitchenAOrders.TryDequeue(out var order);
            return order;
        }
    }
}
