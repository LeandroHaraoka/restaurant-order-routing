using RestaurantOrderRouting.Domain.Aggregates.Orders;
using RestaurantOrderRouting.Domain.ValueObjects;
using RestaurantOrderRouting.Infrastructure.Logging;
using System;
using System.Threading.Tasks;

namespace RestaurantOrderRouting.Services.Handlers
{
    /// <summary>
    /// Contract for handler that defines remove order isntructions.
    /// </summary>
    public interface IRemoveOrderHandler
    {
        Task<HandlerResponse<HandlerResult, Order>> Handle(KitchenArea kitchenArea);
    }

    /// <summary>
    /// Handler responsible for removing an order in the spefic kitchen area.
    /// </summary>
    internal sealed class RemoveOrderHandler : IRemoveOrderHandler
    {
        private readonly IOrderReader _orderReader;
        public RemoveOrderHandler(IOrderReader orderReader) => _orderReader = orderReader;

        public async Task<HandlerResponse<HandlerResult, Order>> Handle(KitchenArea kitchenArea)
        {
            try
            {
                var order = await _orderReader.Dequeue(kitchenArea);
                return order is { }
                    ? HandlerResponse<HandlerResult, Order>.CreateSuccessResult(order)
                    : new HandlerResponse<HandlerResult, Order>(HandlerResult.NoContent, string.Empty, default);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return HandlerResponse<HandlerResult, Order>.CreateErrorResult(ex.Message);
            }
        }
    }
}
