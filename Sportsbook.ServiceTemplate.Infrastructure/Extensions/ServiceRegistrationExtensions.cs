using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sportsbook.ServiceTemplate.Application.Interfaces;
using Sportsbook.ServiceTemplate.Infrastructure.Repositories;

namespace Sportsbook.ServiceTemplate.Infrastructure.Extensions;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
    {
        var cs = configuration.GetConnectionString("InventoryDb");
        Console.WriteLine("====================================");
        Console.WriteLine($"InventoryDb connection string: {cs}");
        Console.WriteLine("====================================");
        services.AddDbContext<InventoryDbContext>(options =>
             options.UseNpgsql(
                 configuration.GetConnectionString("InventoryDb")));

        services.AddScoped<IInventoryRepository, InventoryRepository>();

        return services;
    }
}