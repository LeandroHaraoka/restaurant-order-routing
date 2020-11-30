using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using RestaurantOrderRouting.Domain.Aggregates.Orders;
using RestaurantOrderRouting.Services.Messages;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantOrderRouting.Tests.Integration.Orders
{

    public sealed class CreateOrderTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public CreateOrderTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Given_a_valid_request_should_create_order_sucessfully()
        {
            //Arrange
            var request = new Fixture().Create<CreateOrderRequest>();

            //Act
            var result = await _client.PostAsync("/orders",
                new StringContent(
                    JsonConvert.SerializeObject(request),
                    Encoding.UTF8,
                    "application/json"));
            
            var resultAsString = result.Content.ReadAsStringAsync().Result;
            var resultObject = JsonConvert.DeserializeObject<Order>(resultAsString);

            //Assert
            result.IsSuccessStatusCode.Should().BeTrue();
            resultObject.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Given_a_invalid_request_should_return_bad_request()
        {
            //Arrange
            var request = new CreateOrderRequest();
            //Act
            var result = await _client.PostAsync("/orders",
                new StringContent(
                    JsonConvert.SerializeObject(request),
                    Encoding.UTF8,
                    "application/json"));

            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
