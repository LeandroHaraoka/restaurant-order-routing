using AutoFixture;
using FluentAssertions;
using RestaurantOrderRouting.Domain.Aggregates.Orders;
using RestaurantOrderRouting.Domain.Aggregates.Orders.Queues;
using RestaurantOrderRouting.Domain.ValueObjects;
using RestaurantOrderRouting.Tests.Unit.Domain.Aggregates.Orders.Factories;
using RestaurantOrderRouting.Tests.Unit.Domain.Aggregates.Orders.Seeds;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantOrderRouting.Tests.Unit.Domain.Aggregates.Orders
{
    public sealed class OrderReaderTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public async Task Given_reader_with_no_queue_should_throw_exception()
        {
            // Arrange
            var queueEmptyColeection = new ReadOnlyCollection<IOrderQueue>(new List<IOrderQueue>());
            var orderReader = new OrderReader(queueEmptyColeection);
            var kitchenArea = _fixture.Create<KitchenArea>();

            // Act & Assertion
            await Assert.ThrowsAsync<Exception>(async () => await orderReader.Dequeue(kitchenArea));
        }

        [Theory]
        [MemberData(nameof(OrderTestsSeed.Data), MemberType = typeof(OrderTestsSeed))]
        public async Task Given_reader_with_queues_should_succesfully_dequeue(KitchenArea kitchenArea, Order order)
        {
            // Arrange
            var queues = OrderQueueFactory.CreateOrderQueues().ToList();
            await queues.Single(x => x.KitchenArea == kitchenArea).Enqueue(order);
            var orderReader = new OrderReader(queues);

            // Act
            var dequeuedOrder = await orderReader.Dequeue(kitchenArea);

            // Assert
            dequeuedOrder.Should().NotBeNull();
            dequeuedOrder.Should().Be(order);
        }

        [Theory]
        [InlineData(KitchenArea.KitchenA)]
        [InlineData(KitchenArea.KitchenB)]
        [InlineData(KitchenArea.KitchenC)]
        [InlineData(KitchenArea.KitchenD)]
        [InlineData(KitchenArea.KitchenE)]
        public async Task Given_reader_with_empty_queues_should_succesfully_dequeue(KitchenArea kitchenArea)
        {
            // Arrange
            var queues = OrderQueueFactory.CreateOrderQueues().ToList();
            var orderReader = new OrderReader(queues);

            // Act
            var dequeuedOrder = await orderReader.Dequeue(kitchenArea);

            // Assert
            dequeuedOrder.Should().BeNull();
        }
    }
}
