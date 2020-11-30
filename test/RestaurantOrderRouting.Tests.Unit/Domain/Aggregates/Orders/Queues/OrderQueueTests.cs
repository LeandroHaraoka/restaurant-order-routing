using FluentAssertions;
using RestaurantOrderRouting.Domain.Aggregates.Orders;
using RestaurantOrderRouting.Domain.Aggregates.Orders.Queues;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantOrderRouting.Tests.Unit.Domain.Aggregates.Orders.Queues
{
    public sealed class OrderQueueTests
    {
        [Theory]
        [MemberData(nameof(OrderQueueTestsSeed.Data), MemberType = typeof(OrderQueueTestsSeed))]
        public async Task Given_empty_queue_should_add_order(Type orderQueueType, Order order)
        {
            // Arrange
            var queue = (IOrderQueue)Activator.CreateInstance(orderQueueType);
            
            // Act
            await queue.Enqueue(order);
            
            // Assert
            var addedOrder = await queue.Dequeue();
            addedOrder.Should().NotBeNull();
            addedOrder.Should().BeEquivalentTo(order);
        }
    }
}
