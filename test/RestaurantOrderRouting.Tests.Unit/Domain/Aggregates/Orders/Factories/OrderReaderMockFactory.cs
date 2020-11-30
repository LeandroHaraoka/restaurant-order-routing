using AutoFixture;
using Moq;
using RestaurantOrderRouting.Domain.Aggregates.Orders;
using RestaurantOrderRouting.Domain.ValueObjects;

namespace RestaurantOrderRouting.Tests.Unit.Domain.Aggregates.Orders.Factories
{
    /// <summary>
    /// Provides an mock of IOrderReader with an initial setup.
    /// </summary>
    public class OrderReaderMockFactory
    {
        public readonly Mock<IOrderReader> OrderReaderMock;

        public OrderReaderMockFactory()
        {
            OrderReaderMock = new Mock<IOrderReader>();
            OrderReaderMock
                .Setup(x => x.Dequeue(It.IsAny<KitchenArea>()))
                .ReturnsAsync(new Fixture().Create<Order>());
        }
    }
}
