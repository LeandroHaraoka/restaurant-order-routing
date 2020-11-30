using AutoFixture;
using FluentAssertions;
using RestaurantOrderRouting.Domain.Aggregates.Orders.Queues;
using RestaurantOrderRouting.Domain.ValueObjects;
using RestaurantOrderRouting.Tests.Unit.Domain.Aggregates.Orders.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantOrderRouting.Tests.Unit.Domain.Aggregates.Orders.Queues
{
    public sealed class OrderQueueExtensionsTests
    {
        [Fact]
        public async Task Given_empty_queue_collection_should_throw_exception()
        {
            // Arrange
            var queues = new List<IOrderQueue>();
            
            // Act & Assertion
            Assert.Throws<Exception>(() => queues.GetQueue(new Fixture().Create<KitchenArea>()));
        }

        [Theory]
        [InlineData(KitchenArea.KitchenA)]
        [InlineData(KitchenArea.KitchenB)]
        [InlineData(KitchenArea.KitchenC)]
        [InlineData(KitchenArea.KitchenD)]
        [InlineData(KitchenArea.KitchenE)]
        public async Task Given_queue_collection_should_retrieve_specific_queue(KitchenArea kitchenArea)
        {
            // Arrange
            var queues = OrderQueueFactory.CreateOrderQueues().ToList();

            // Act
            var retrievedQueue = queues.GetQueue(kitchenArea);

            // Assertion
            retrievedQueue.Should().NotBeNull();
            retrievedQueue.KitchenArea.Should().Be(kitchenArea);
        }
    }
}
