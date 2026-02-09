using Microsoft.Extensions.DependencyInjection;
using Sportsbook.InventoryService.Application.Features.InventoryItemContext.GetAll;
using Sportsbook.InventoryService.Application.Features.InventoryItemContext.AddStock;
using Sportsbook.InventoryService.Application.Features.InventoryItemContext.CreateItem;
using Sportsbook.InventoryService.Application.Features.InventoryItemContext.GetBySku;
using Sportsbook.InventoryService.Application.Features.InventoryItemContext.RemoveStock;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;

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

        return services;
    }

    /// <summary>
    /// Insert Fluent Validation dependencies here...
    /// </summary>
    public static IServiceCollection AddValidationServices(this IServiceCollection services)
    {
        // e.g.: builder.Services.AddScoped<IValidator<Person>, PersonValidator>();

        return services;
    }


}