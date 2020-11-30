using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using RestaurantOrderRouting.Domain.Aggregates.Orders;
using RestaurantOrderRouting.Domain.ValueObjects;
using RestaurantOrderRouting.Services.Messages;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantOrderRouting.Tests.Integration.Orders
{
    public sealed class RemoveOrdersTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public RemoveOrdersTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Given_an_empty_queue_should_return_no_content()
        {
            //Arrange
            var kitchenArea = new Fixture().Create<KitchenArea>();

            //Act
            var queryString = new Dictionary<string, string> { { "kitchenArea", kitchenArea.ToString() } };

            var result = await _client.DeleteAsync(QueryHelpers.AddQueryString("/orders", queryString));

            //Assert
            result.IsSuccessStatusCode.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Given_not_empty_queue_should_remove_order_succesfully()
        {
            //Arrange
            var createOrderRequest = new Fixture().Create<CreateOrderRequest>();
            var kitchenArea = createOrderRequest.KitchenArea;
            await _client.PostAsync("/orders",
                new StringContent(
                    JsonConvert.SerializeObject(createOrderRequest),
                    Encoding.UTF8,
                    "application/json"));

            //Act
            var queryString = new Dictionary<string, string> { { "kitchenArea", kitchenArea.ToString() } };
            var result = await _client.DeleteAsync(QueryHelpers.AddQueryString("/orders", queryString));
            var resultAsString = result.Content.ReadAsStringAsync().Result;
            var resultObject = JsonConvert.DeserializeObject<Order>(resultAsString);

            //Assert
            result.IsSuccessStatusCode.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            resultObject.Should().NotBeNull();
            resultObject.Id.Should().NotBeEmpty();
        }
    }
}
