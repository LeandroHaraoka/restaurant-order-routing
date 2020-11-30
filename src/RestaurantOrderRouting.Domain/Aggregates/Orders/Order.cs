using RestaurantOrderRouting.Domain.ValueObjects;
using System;

namespace RestaurantOrderRouting.Domain.Aggregates.Orders
{
    /// <summary>
    /// Order contract that represents an order that is routed to the kitchen.
    /// </summary>
    public sealed class Order
    {
        public Guid Id { get; }
        public DateTime OrderDate { get; }
        public decimal Price { get; }
        public string Description { get; }
        public KitchenArea KitchenArea { get; }

        public Order(DateTime orderDate, decimal price, string description, KitchenArea kitchenArea)
        {
            Id = Guid.NewGuid();
            OrderDate = orderDate;
            Price = price;
            Description = description;
            KitchenArea = kitchenArea;
        }
    }
}
