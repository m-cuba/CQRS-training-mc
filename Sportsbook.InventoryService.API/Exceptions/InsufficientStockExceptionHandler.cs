using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sportsbook.InventoryService.Core.Exceptions;

namespace Sportsbook.InventoryService.API.Exceptions
{
    internal sealed class InsufficientStockExceptionHandler(
        ILogger<InsufficientStockExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is not InsufficientStockException insufficientStockException)
            {
                return false;
            }

            logger.LogWarning(
                exception,
                "Insufficient stock for SKU {Sku}",
                insufficientStockException.Sku.Value);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Insufficient stock",
                Detail = exception.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
