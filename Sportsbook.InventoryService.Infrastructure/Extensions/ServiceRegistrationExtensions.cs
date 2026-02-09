using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Core.Repositories;
using Sportsbook.InventoryService.Infrastructure.Repositories;

namespace Sportsbook.InventoryService.Infrastructure.Extensions;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InventoryDbContext>(options =>
             options.UseNpgsql(
                 configuration.GetConnectionString("InventoryDb")));

        services.AddScoped<IInventoryRepository, InventoryRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<InventoryDbContext>());

        return services;
    }
}