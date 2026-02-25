using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using InventoryService.Audit.Application.Seedwork.Interfaces;
using InventoryService.Audit.Core;
using InventoryService.Audit.Infrastructure.Persistence;
using InventoryService.Audit.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryService.Audit.Infrastructure.Extensions;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
    {
         services.AddDbContext<AuditDbContext>(o =>
            o.UseNpgsql(configuration.GetConnectionString("AuditDb")));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AuditDbContext>());

        return services.AddRepositories()
            .AddPulsar(configuration);
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services.AddScoped<IAuditStockRepository, AuditStockRepository>();
    }

    private static IServiceCollection AddPulsar(this IServiceCollection services, IConfiguration configuration)
    {
        string pulsarUrl = configuration["Pulsar:ServiceUrl"];
        if (string.IsNullOrWhiteSpace(pulsarUrl))
        {
            pulsarUrl = "pulsar://pulsar:6650";
        }

        services.AddSingleton(sp =>
        {
            return PulsarClient.Builder()
                .ServiceUrl(new Uri(pulsarUrl))
                .Build();
        });

        services.AddSingleton(sp =>
        {
            IPulsarClient client = sp.GetRequiredService<IPulsarClient>();
            return client.NewConsumer()
                .Topic("inventory.stock-events")
                .SubscriptionName("inventory-projections")
                .Create();
        });

        return services;
    }
}