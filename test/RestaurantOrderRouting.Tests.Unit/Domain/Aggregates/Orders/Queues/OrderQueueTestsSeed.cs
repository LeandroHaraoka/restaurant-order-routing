using AutoFixture;
using RestaurantOrderRouting.Domain.Aggregates.Orders;
using RestaurantOrderRouting.Domain.Aggregates.Orders.Queues;
using System;
using Xunit;

namespace RestaurantOrderRouting.Tests.Unit.Domain.Aggregates.Orders.Queues
{
    /// <summary>
    /// Data seed for theory tests of orderqueue services.
    /// </summary>
    internal sealed class OrderQueueTestsSeed : TheoryData<Type, Order>
    {
        public static TheoryData<Type, Order> Data
        {
            get
            {
                var types = IOrderQueueImplementations.GetAllTypes();
                var fixture = new Fixture();
                var result = new TheoryData<Type, Order>();
                
                foreach (var type in types)
                    result.Add(type, fixture.Create<Order>());

                return result;
            }
        }
    }
}
