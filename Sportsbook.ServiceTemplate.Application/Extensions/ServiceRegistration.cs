using Microsoft.Extensions.DependencyInjection;
using Sportsbook.ServiceTemplate.Application.Commands.AddStock;
using Sportsbook.ServiceTemplate.Application.Commands.CreateItem;
using Sportsbook.ServiceTemplate.Application.Commands.RemoveStock;
using Sportsbook.ServiceTemplate.Application.Interfaces;
using Sportsbook.ServiceTemplate.Application.Queries.GetAllItems;
using Sportsbook.ServiceTemplate.Application.Queries.GetItemBySku;

namespace Sportsbook.ServiceTemplate.Application.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddScoped<ICreateItemCommandHandler, CreateItemCommandHandler>();
        services.AddScoped<IAddStockCommandHandler, AddStockCommandHandler>();
        services.AddScoped<IRemoveStockCommandHandler, RemoveStockCommandHandler>();
        services.AddScoped<IGetItemBySkuQueryHandler, GetItemBySkuQueryHandler>();
        services.AddScoped<IGetAllItemsQueryHandler, GetAllItemsQueryHandler>();

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