using FluentValidation;

namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.AddStock
{
    internal sealed class AddStockCommandValidator: AbstractValidator<AddStockCommand>
    {
        public AddStockCommandValidator()
        {
            RuleFor(x => x.Sku)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
