using FluentValidation;

namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.RemoveStock
{
    internal sealed class RemoveStockCommandValidator : AbstractValidator<RemoveStockCommand>
    {
        public RemoveStockCommandValidator()
        {
            RuleFor(x => x.Sku)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}
