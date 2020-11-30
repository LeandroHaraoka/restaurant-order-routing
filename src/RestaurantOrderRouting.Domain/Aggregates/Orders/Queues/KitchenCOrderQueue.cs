using RestaurantOrderRouting.Domain.Aggregates.Orders.Queues;
using RestaurantOrderRouting.Domain.ValueObjects;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace RestaurantOrderRouting.Domain.Aggregates.Orders.Queuess
{
    /// <summary>
    /// KitchenAreaC implementation of IOrderQueue. Manages all orders that contain KitchenArea = KitchenC.
    /// </summary>
    public sealed class KitchenCOrderQueue : IOrderQueue
    {
        public KitchenArea KitchenArea => KitchenArea.KitchenC;
        private readonly ConcurrentQueue<Order> _kitchenCOrders;

        public KitchenCOrderQueue()
        {
            _kitchenCOrders = new ConcurrentQueue<Order>();
        }

        public async Task<Order> Enqueue(Order order)
        {
            _kitchenCOrders.Enqueue(order);
            return order;
        }

        public async Task<Order> Dequeue()
        {
            _kitchenCOrders.TryDequeue(out var order);
            return order;
        }
    }
}
