using RestaurantOrderRouting.Domain.Aggregates.Orders.Queues;
using RestaurantOrderRouting.Domain.ValueObjects;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace RestaurantOrderRouting.Domain.Aggregates.Orders.Queuess
{
    /// <summary>
    /// KitchenAreaE implementation of IOrderQueue. Manages all orders that contain KitchenArea = KitchenE.
    /// </summary>
    public sealed class KitchenEOrderQueue : IOrderQueue
    {
        public KitchenArea KitchenArea => KitchenArea.KitchenE;
        private readonly ConcurrentQueue<Order> _kitchenEOrders;

        public KitchenEOrderQueue()
        {
            _kitchenEOrders = new ConcurrentQueue<Order>();
        }

        public async Task<Order> Enqueue(Order order)
        {
            _kitchenEOrders.Enqueue(order);
            return order;
        }

        public async Task<Order> Dequeue()
        {
            _kitchenEOrders.TryDequeue(out var order);
            return order;
        }
    }
}
