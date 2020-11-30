using RestaurantOrderRouting.Domain.Aggregates.Orders;
using RestaurantOrderRouting.Infrastructure.Logging;
using RestaurantOrderRouting.Services.Messages;
using System;
using System.Threading.Tasks;

namespace RestaurantOrderRouting.Services.Handlers
{
    /// <summary>
    /// Contract for handler that defines create order isntructions.
    /// </summary>
    public interface ICreateOrderHandler
    {
        Task<HandlerResponse<HandlerResult, Order>> Handle(CreateOrderRequest request);
    }

    /// <summary>
    /// Handler responsible for creating an order in the spefic kitchen area.
    /// </summary>
    internal sealed class CreateOrderHandler : ICreateOrderHandler
    {
        private readonly IOrderWriter _orderWriter;
        public CreateOrderHandler(IOrderWriter orderWriter) => _orderWriter = orderWriter;

        public async Task<HandlerResponse<HandlerResult, Order>> Handle(CreateOrderRequest request)
        {
            try
            {
                var order = request.CreateOrder();
                var addedOrder = await _orderWriter.Add(order);
                return HandlerResponse<HandlerResult, Order>.CreateSuccessResult(addedOrder);
            }
            catch (ArgumentNullException ex)
            {
                Logger.LogException(ex);
                return HandlerResponse<HandlerResult, Order>.CreatePreConditionUnsatisfiedResult(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return HandlerResponse<HandlerResult, Order>.CreateErrorResult(ex.Message);
            }
        }
    }
}
