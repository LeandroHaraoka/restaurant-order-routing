using AutoFixture;
using FluentAssertions;
using Moq;
using RestaurantOrderRouting.Domain.Aggregates.Orders;
using RestaurantOrderRouting.Domain.ValueObjects;
using RestaurantOrderRouting.Services.Handlers;
using RestaurantOrderRouting.Tests.Unit.Domain.Aggregates.Orders.Factories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantOrderRouting.Tests.Unit.Services.Handlers
{
    public sealed class RemoveOrderHandlerTests : IClassFixture<OrderReaderMockFactory>
    {
        private readonly Mock<IOrderReader> _orderReaderMock;
        public RemoveOrderHandlerTests(OrderReaderMockFactory orderReaderMockFactory)
        {
            _orderReaderMock = orderReaderMockFactory.OrderReaderMock;
        }

        [Fact]
        public async Task Given_an_exception_should_return_error()
        {
            // Arrange
            _orderReaderMock
                .Setup(x => x.Dequeue(It.IsAny<KitchenArea>()))
                .ThrowsAsync(new Exception("Exception message."));
            var handler = new RemoveOrderHandler(_orderReaderMock.Object);

            // Act
            var handlerResponse = await handler.Handle(new Fixture().Create<KitchenArea>());

            // Assertion
            handlerResponse.Result.Should().Be(HandlerResult.Error);
            handlerResponse.Message.Should().NotBeNullOrWhiteSpace();
            handlerResponse.Data.Should().Be(default);
        }

        [Fact]
        public async Task Given_null_dequeued_order_should_return_no_content()
        {
            // Arrange
            _orderReaderMock
                .Setup(x => x.Dequeue(It.IsAny<KitchenArea>()))
                .ReturnsAsync(default(Order));
            var handler = new RemoveOrderHandler(_orderReaderMock.Object);

            // Act
            var handlerResponse = await handler.Handle(new Fixture().Create<KitchenArea>());

            // Assertion
            handlerResponse.Result.Should().Be(HandlerResult.NoContent);
            handlerResponse.Message.Should().BeEmpty();
            handlerResponse.Data.Should().Be(default);
        }

        [Fact]
        public async Task Given_valid_dequeued_order_should_return_success()
        {
            // Arrange
            var handler = new RemoveOrderHandler(_orderReaderMock.Object);

            // Act
            var handlerResponse = await handler.Handle(new Fixture().Create<KitchenArea>());

            // Assertion
            handlerResponse.Result.Should().Be(HandlerResult.Success);
            handlerResponse.Message.Should().BeEmpty();
            handlerResponse.Data.Should().NotBeNull();
        }
    }
}
