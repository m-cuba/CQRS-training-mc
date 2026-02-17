using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Sportsbook.InventoryService.Application.Features.InventoryItemContext.AddStock;
using Sportsbook.InventoryService.Application.Features.InventoryItemContext.CreateItem;
using Sportsbook.InventoryService.Application.Features.InventoryItemContext.GetAll;
using Sportsbook.InventoryService.Application.Features.InventoryItemContext.GetAudit;
using Sportsbook.InventoryService.Application.Features.InventoryItemContext.GetBySku;
using Sportsbook.InventoryService.Application.Features.InventoryItemContext.GetLowStock;
using Sportsbook.InventoryService.Application.Features.InventoryItemContext.RemoveStock;
using Sportsbook.InventoryService.Application.Features.InventoryItemContext.StockChanged;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Application.Seedwork.Services;

namespace Sportsbook.InventoryService.Application.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICreateItemCommandHandler, CreateItemCommandHandler>();
        services.AddScoped<IAddStockCommandHandler, AddStockCommandHandler>();
        services.AddScoped<IRemoveStockCommandHandler, RemoveStockCommandHandler>();
        services.AddScoped<IGetItemBySkuQueryHandler, GetBySkuQueryHandler>();
        services.AddScoped<IGetAllItemsQueryHandler, GetAllQueryHandler>();
        services.AddScoped<IGetLowStockItemsQueryHandler, GetLowStockQueryHandler>();
        services.AddScoped<IGetStockAuditQueryHandler, GetStockAuditQueryHandler>();
        services.AddScoped<IStockChangedHandler, StockChangedHandler>();

        return services
            .AddValidationServices()
            .AddServices();
    }

    private static IServiceCollection AddValidationServices(this IServiceCollection services)
    {
       return services.AddScoped<IValidator<AddStockCommand>, AddStockCommandValidator>()
            .AddScoped<IValidator<RemoveStockCommand>, RemoveStockCommandValidator>()
            .AddScoped<IValidator<CreateItemCommand>, CreateItemCommandValidator>();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IStockMovementService, StockMovementService>();        
    }
}