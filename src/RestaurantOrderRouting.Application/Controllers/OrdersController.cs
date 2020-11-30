using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderRouting.Domain.Aggregates.Orders;
using RestaurantOrderRouting.Domain.ValueObjects;
using RestaurantOrderRouting.Services.Handlers;
using RestaurantOrderRouting.Services.Messages;
using System.Threading.Tasks;

namespace RestaurantOrderRouting.Application.Controllers
{
    [Route("orders")]
    [ApiController]
    public sealed class OrdersController : ControllerBase
    {
        private readonly ICreateOrderHandler _createOrderHandler;
        private readonly IRemoveOrderHandler _removeOrderHandler;
        
        public OrdersController(ICreateOrderHandler createOrderHandler,
            IRemoveOrderHandler removeOrderHandler)
        {
            _createOrderHandler = createOrderHandler;
            _removeOrderHandler = removeOrderHandler; 
        }

        /// <summary>
        /// Routs restaurant orders to specific areas of a kitchen.
        /// </summary>
        /// <param name="request">Create order request.</param>
        /// <response code="200">Successful response.</response>
        /// <response code="400">Client request is invalid.</response>
        /// <response code="422">Order cannot be created for the received request.</response>
        /// <response code="500">Internal Server Error.</response>
        [HttpPost()]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<object>> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var createOrderResponse = await _createOrderHandler.Handle(request);

            return CreateActionResult<Order>.Create(createOrderResponse);
        }

        /// <summary>
        /// Remove the first order from queue of kitchen specific area.
        /// </summary>
        /// <param name="request">Remove order request.</param>
        /// <response code="200">Successful response.</response>
        /// <response code="204">No order was found in the specified kitchen area.</response>
        /// <response code="400">Client request is invalid.</response>
        /// <response code="422">Order cannot be peeked for the received request.</response>
        /// <response code="500">Internal Server Error.</response>
        [HttpDelete()]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<object>> RemoveOrder([FromQuery] KitchenArea kitchenArea)
        {
            var removeOrderResponse = await _removeOrderHandler.Handle(kitchenArea);

            return CreateActionResult<Order>.Create(removeOrderResponse);
        }
    }
}
