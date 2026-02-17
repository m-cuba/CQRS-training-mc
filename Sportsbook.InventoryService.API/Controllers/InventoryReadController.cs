using Microsoft.AspNetCore.Mvc;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Application.Seedwork.Responses;

namespace Sportsbook.InventoryService.API.Controllers
{
    public class InventoryReadController(
        IGetLowStockItemsQueryHandler getLowStockHandler,
        IGetStockAuditQueryHandler getStockAuditHandler) : ApiController
    {
        [HttpGet("low-stock")]
        [ProducesResponseType(typeof(List<InventoryItemResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<InventoryItemResponse>>> GetLowStock([FromQuery] int threshold = 10, CancellationToken cancellationToken = default)
        {
            var items = await getLowStockHandler.Handle(threshold, cancellationToken);
            return Ok(items);
        }

        [HttpGet("{sku}/audit")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<string>>> GetAudit(string sku, CancellationToken cancellationToken = default)
        {
            var logs = await getStockAuditHandler.Handle(sku, cancellationToken);
            return Ok(logs);
        }
    }
}