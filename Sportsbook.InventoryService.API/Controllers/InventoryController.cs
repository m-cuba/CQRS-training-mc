using Microsoft.AspNetCore.Mvc;
using Sportsbook.InventoryService.API.Request;
using Sportsbook.InventoryService.Application.Commands.AddStock;
using Sportsbook.InventoryService.Application.Commands.CreateItem;
using Sportsbook.InventoryService.Application.Commands.RemoveStock;
using Sportsbook.InventoryService.Application.Queries.GetItemBySku;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Application.Seedwork.Responses;
using Sportsbook.InventoryService.Core.Exceptions;
using System.Net;

namespace Sportsbook.InventoryService.API.Controllers
{
    public class InventoryController(
        ICreateItemCommandHandler createItemHandler,
        IAddStockCommandHandler addStockHandler,
        IRemoveStockCommandHandler removeStockHandler,
        IGetItemBySkuQueryHandler getItemBySkuHandler,
        IGetAllItemsQueryHandler getAllItemsHandler) : ApiController
    {
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateItemCommand(
                request.Sku,
                request.Name);

            await createItemHandler.Handle(command, cancellationToken);

            return CreatedAtAction(
                nameof(GetBySku),
                new { sku = request.Sku },
                null);
        }

        [HttpPost("{sku}/add-stock")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> AddStock(string sku, [FromBody] AddStockRequest request, CancellationToken cancellationToken)
        {
            await addStockHandler.Handle(new AddStockCommand(sku, request.Quantity), cancellationToken); 

            return NoContent();
        }

        [HttpPost("{sku}/remove-stock")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> RemoveStock(string sku, [FromBody] RemoveStockRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await removeStockHandler.Handle(new RemoveStockCommand(sku, request.Quantity), cancellationToken);

                return NoContent();
            }
            catch (InsufficientStockException ex)
            {
                return Conflict(new { ex.Message });
            }
        }

        [HttpGet("{sku}")]
        [ProducesResponseType(typeof(InventoryItemResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<InventoryItemResponse>> GetBySku(string sku, CancellationToken cancellationToken)
        {
            var result = await getItemBySkuHandler.Handle(new GetItemBySkuQuery(sku), cancellationToken);

            return result is null
                ? NotFound()
                : Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<InventoryItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<InventoryItemResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await getAllItemsHandler.Handle(cancellationToken);
            return Ok(result);
        }
    }
}
