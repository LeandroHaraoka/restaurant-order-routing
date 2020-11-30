using AutoFixture;
using RestaurantOrderRouting.Domain.Aggregates.Orders;
using RestaurantOrderRouting.Domain.ValueObjects;
using Xunit;

namespace RestaurantOrderRouting.Tests.Unit.Domain.Aggregates.Orders.Seeds
{
    /// <summary>
    /// Data seed for theory tests of order services.
    /// </summary>
    internal sealed class OrderTestsSeed : TheoryData<KitchenArea, Order>
    {
        public static TheoryData<KitchenArea, Order> Data
        {
            get
            { 
                var fixture = new Fixture();
                return new TheoryData<KitchenArea, Order>
                {
                    { KitchenArea.KitchenA, fixture.Create<Order>()},
                    { KitchenArea.KitchenB, fixture.Create<Order>()},
                    { KitchenArea.KitchenC, fixture.Create<Order>()},
                    { KitchenArea.KitchenD, fixture.Create<Order>()},
                    { KitchenArea.KitchenE, fixture.Create<Order>()},
                };
            }
        }
    }
}
