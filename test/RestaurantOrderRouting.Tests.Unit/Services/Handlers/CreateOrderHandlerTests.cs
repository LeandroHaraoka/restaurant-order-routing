using AutoFixture;
using FluentAssertions;
using Moq;
using RestaurantOrderRouting.Domain.Aggregates.Orders;
using RestaurantOrderRouting.Services.Handlers;
using RestaurantOrderRouting.Services.Messages;
using RestaurantOrderRouting.Tests.Unit.Domain.Aggregates.Orders.Factories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantOrderRouting.Tests.Unit.Services.Handlers
{
    public sealed class CreateOrderHandlerTests : IClassFixture<OrderWriterMockFactory>
    {
        private readonly Mock<IOrderWriter> _orderWriterMock;

        public CreateOrderHandlerTests(OrderWriterMockFactory orderWriterMockFactory)
        {
            _orderWriterMock = orderWriterMockFactory.OrderWriterMock;
        }

        [Fact]
        public async Task Given_an_argument_null_expetion_should_return_precondition_unsatisfied()
        {
            // Arrange
            var request = new CreateOrderRequest();
            var handler = new CreateOrderHandler(_orderWriterMock.Object);

            // Act
            var handlerResponse = await handler.Handle(request);

            // Assertion
            handlerResponse.Result.Should().Be(HandlerResult.PreConditionUnsatisfied);
            handlerResponse.Message.Should().NotBeNullOrWhiteSpace();
            handlerResponse.Data.Should().Be(default);
        }

        [Fact]
        public async Task Given_an_exception_should_return_error()
        {
            // Arrange
            _orderWriterMock
                .Setup(x => x.Add(It.IsAny<Order>()))
                .ThrowsAsync(new Exception("Exception message."));

            var handler = new CreateOrderHandler(_orderWriterMock.Object);
            var request = new Fixture().Create<CreateOrderRequest>();

            // Act
            var handlerResponse = await handler.Handle(request);

            // Assertion
            handlerResponse.Result.Should().Be(HandlerResult.Error);
            handlerResponse.Message.Should().NotBeNullOrWhiteSpace();
            handlerResponse.Data.Should().Be(default);
        }

        [Fact]
        public async Task Given_a_valid_request_should_add_and_return_success()
        {
            // Arrange
            var fixture = new Fixture();
            var handler = new CreateOrderHandler(_orderWriterMock.Object);
            var request = fixture.Create<CreateOrderRequest>();

            // Act
            var handlerResponse = await handler.Handle(request);

            // Assertion
            handlerResponse.Result.Should().Be(HandlerResult.Success);
            handlerResponse.Message.Should().BeEmpty();
            handlerResponse.Data.Should().NotBeNull();
        }
    }
}
