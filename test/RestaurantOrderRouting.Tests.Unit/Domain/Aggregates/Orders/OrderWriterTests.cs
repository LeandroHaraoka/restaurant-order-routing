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
    public sealed class OrderWriterTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public async Task Given_writer_with_no_queue_should_throw_exception()
        {
            // Arrange
            var queueEmptyColeection = new ReadOnlyCollection<IOrderQueue>(new List<IOrderQueue>());
            var orderWriter = new OrderWriter(queueEmptyColeection);
            var kitchenArea = _fixture.Create<KitchenArea>();

            // Act & Assertion
            await Assert.ThrowsAsync<Exception>(async () => await orderWriter.Add(_fixture.Create<Order>()));
        }

        [Theory]
        [MemberData(nameof(OrderTestsSeed.Data), MemberType = typeof(OrderTestsSeed))]
        public async Task Given_writer_with_empty_queues_should_add_one_order(KitchenArea kitchenArea, Order order)
        {
            // Arrange
            var queues = OrderQueueFactory.CreateOrderQueues().ToList();
            var orderWriter = new OrderWriter(queues);

            // Act
            await orderWriter.Add(order);

            // Assert
            var addedOrder = await queues.Single(x => x.KitchenArea == kitchenArea).Dequeue();
            addedOrder.Should().NotBeNull();
            addedOrder.Should().BeEquivalentTo(order);
        }
    }
}
