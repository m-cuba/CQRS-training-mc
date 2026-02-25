using InventoryService.Audit.Application.Seedwork.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Sportsbook.InventoryService.Audit.API.Response;

namespace Sportsbook.InventoryService.Audit.API.Controllers
{
    [ApiController]
    [Route("audit")]
    public class AuditController(IGetStockHandler handler) : ControllerBase
    {
        [HttpGet("stock")]
        public async Task<IActionResult> GetStock(
            [FromQuery] string sku,
            [FromQuery] DateTimeOffset? date,
            CancellationToken ct)
        {
            var effectiveDate = date ?? DateTimeOffset.UtcNow;

            var stock = await handler.HandleAsync(sku, effectiveDate, ct);

            return Ok(new StockAtDateResponse(sku, effectiveDate, stock));
        }
    }
}
