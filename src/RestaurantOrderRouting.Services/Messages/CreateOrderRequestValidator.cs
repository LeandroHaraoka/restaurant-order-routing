using FluentValidation;

namespace RestaurantOrderRouting.Services.Messages
{
    /// <summary>
    /// Defines validation rules to CreateOrderRequest.
    /// </summary>
    internal sealed class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.OrderDate)
                .NotEmpty()
                .WithMessage($"'{nameof(CreateOrderRequest.OrderDate)}' must be informed.");
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage($"'{nameof(CreateOrderRequest.Price)}' must be greater than or equal to 0.");
            RuleFor(x => x.Description)
             .NotEmpty()
             .WithMessage($"'{nameof(CreateOrderRequest.Description)}' must be informed.");
            RuleFor(x => x.KitchenArea)
             .NotNull()
             .IsInEnum()
             .WithMessage($"'{nameof(CreateOrderRequest.KitchenArea)}' must be a valid value.");
        }
    }
}
