using RestaurantOrderRouting.Domain.Aggregates.Orders;
using RestaurantOrderRouting.Domain.ValueObjects;
using System;

namespace RestaurantOrderRouting.Services.Messages
{
    /// <summary>
    /// Contract that must be received in application layer to create an order;
    /// </summary>
    public struct CreateOrderRequest
    {
        /* To avoid premature complexity, quantities and ingredients were simplified
            in a Description info. */

        public DateTime OrderDate { get; }
        public decimal Price { get; }
        public string Description { get; }
        public KitchenArea? KitchenArea { get; }

        public CreateOrderRequest(DateTime orderDate, decimal price, string description, KitchenArea? kitchenArea)
        {
            OrderDate = orderDate;
            Price = price;
            Description = description;
            KitchenArea = kitchenArea;
        }

        /// <summary>
        /// Factory method to create an Order instance from a createOrderRequest.
        /// </summary>
        public Order CreateOrder()
        {
            if (!KitchenArea.HasValue)
                throw new ArgumentNullException(nameof(KitchenArea),
                    $"To create an order, {nameof(CreateOrderRequest)}.{nameof(KitchenArea)} cannot be null.");

            return new Order(OrderDate, Price, Description, KitchenArea.Value);
        }
    }
}
