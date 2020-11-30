using AutoFixture;
using Moq;
using RestaurantOrderRouting.Domain.Aggregates.Orders;

namespace RestaurantOrderRouting.Tests.Unit.Domain.Aggregates.Orders.Factories
{
    /// <summary>
    /// Provides an mock of IOrderWriter with an initial setup.
    /// </summary>
    public class OrderWriterMockFactory
    {
        public readonly Mock<IOrderWriter> OrderWriterMock;

        public OrderWriterMockFactory()
        {
            OrderWriterMock = new Mock<IOrderWriter>();
            OrderWriterMock
                .Setup(x => x.Add(It.IsAny<Order>()))
                .ReturnsAsync(new Fixture().Create<Order>());
        }
    }
}
