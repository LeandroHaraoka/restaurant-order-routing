using Microsoft.AspNetCore.Mvc;
using RestaurantOrderRouting.Services.Handlers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RestaurantOrderRouting.Application.Controllers
{
    /// <summary>
    /// Map the corresponding ActionResult from a HandlerResponse.
    /// </summary>
    internal static class CreateActionResult<TData>
    {
        private static ReadOnlyDictionary<HandlerResult, Func<HandlerResponse<HandlerResult, TData>, ActionResult>> 
            _actionResultDictionary = 
                new ReadOnlyDictionary<HandlerResult, Func<HandlerResponse<HandlerResult, TData>, ActionResult>>(
                    new Dictionary<HandlerResult, Func<HandlerResponse<HandlerResult, TData>, ActionResult>>()
                {
                    { HandlerResult.Success, (response) => new OkObjectResult(response.Data) },
                    { HandlerResult.PreConditionUnsatisfied, 
                        (response) => new UnprocessableEntityObjectResult(response.Message) },
                    { HandlerResult.NoContent,
                        (response) => new NoContentResult() },
                    { HandlerResult.Error,
                        (response) => new UnprocessableEntityObjectResult(response.Message) },
                });

        /// <summary>
        /// Create the corresponding ActionResult.
        /// </summary>
        internal static ActionResult Create(HandlerResponse<HandlerResult, TData> handlerResult)
        {
            var actionResult = _actionResultDictionary[handlerResult.Result];
            return actionResult(handlerResult);
        }
    }
}
