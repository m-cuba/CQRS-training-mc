using InventoryService.Audit.Application.Features.GetStock;
using InventoryService.Audit.Application.Features.StockChanged;
using InventoryService.Audit.Application.Seedwork.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryService.Audit.Application.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IStockChangedAuditHandler, StockChangedAuditHandler>();
        services.AddScoped<IGetStockHandler, GetStockHandler>();

        return services;
    }
}