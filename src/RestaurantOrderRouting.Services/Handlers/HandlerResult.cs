namespace RestaurantOrderRouting.Services.Handlers
{
    /// <summary>
    /// Defines possible results returned by a handler.
    /// </summary>
    public enum HandlerResult 
    {
        Success,
        Error, 
        PreConditionUnsatisfied,
        NoContent
    }
}
