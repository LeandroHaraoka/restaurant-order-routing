using RestaurantOrderRouting.Domain.Aggregates.Orders;

namespace RestaurantOrderRouting.Services.Handlers
{
    /// <summary>
    /// Generic handler response, can be used for all handlers.
    /// </summary>
    public struct HandlerResponse<TResult, TData>
    {
        public TResult Result{ get; }
        public string Message { get; }
        public TData Data { get; }
        
        public HandlerResponse(TResult result, string message, TData data)
        {
            Result = result;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// Factory method for a sucessful result.
        /// </summary>
        public static HandlerResponse<HandlerResult, TData> CreateSuccessResult(TData data) =>
            new HandlerResponse<HandlerResult, TData>(HandlerResult.Success, string.Empty, data);

        /// <summary>
        /// Factory method for a precondition unsatisfied result.
        /// </summary>
        public static HandlerResponse<HandlerResult, TData> CreatePreConditionUnsatisfiedResult(string message) =>
            new HandlerResponse<HandlerResult, TData>(HandlerResult.PreConditionUnsatisfied, message, default);

        /// <summary>
        /// Factory method for an error result.
        /// </summary>
        public static HandlerResponse<HandlerResult, TData> CreateErrorResult(string message) =>
            new HandlerResponse<HandlerResult, TData>(HandlerResult.Error, message, default);
    }
}
