using FluentValidation;

namespace Sportsbook.InventoryService.Application.Features.InventoryItemContext.CreateItem
{
    internal sealed class CreateItemCommandValidator: AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator()
        {
            RuleFor(x => x.Sku)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
