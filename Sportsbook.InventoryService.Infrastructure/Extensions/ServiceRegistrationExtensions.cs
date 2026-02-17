using CloudNative.CloudEvents;
using CloudNative.CloudEvents.SystemTextJson;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Core.InventoryContext.Repositories;
using Sportsbook.InventoryService.Infrastructure.Messaging;
using Sportsbook.InventoryService.Infrastructure.Repositories;

namespace Sportsbook.InventoryService.Infrastructure.Extensions;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InventoryDbContext>(options =>
             options.UseNpgsql(
                 configuration.GetConnectionString("InventoryDb")));

        services.AddDbContext<ReadDbContext>(options =>
             options.UseNpgsql(
                 configuration.GetConnectionString("InventoryReadDb")));

        services.AddScoped<IReadUnitOfWork>(sp => sp.GetRequiredService<ReadDbContext>());
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<InventoryDbContext>());

        return services.AddRepositories()
            .AddPulsar(configuration);
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services.AddScoped<IInventoryRepository, InventoryRepository>()
            .AddScoped<IInventoryReadRepository, InventoryReadRepository>()
            .AddScoped<IStockMovementReadRepository, StockMovementReadRepository>();
    }

    private static IServiceCollection AddPulsar(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<CloudEventFormatter, JsonEventFormatter>();

        string pulsarUrl = configuration.GetValue<string>("Pulsar:ServiceUrl");
        if (string.IsNullOrWhiteSpace(pulsarUrl))
        {
            pulsarUrl = "pulsar://pulsar:6650";
        }

        services.AddSingleton<IPulsarClient>(sp =>
        {
            return PulsarClient.Builder()
                .ServiceUrl(new Uri(pulsarUrl))
                .Build();
        });

        // Register producer for publishing
        services.AddSingleton(sp =>
        {
            IPulsarClient client = sp.GetRequiredService<IPulsarClient>();
            return client.NewProducer()
                .Topic("inventory.stock-events")
                .Create();
        });

        services.AddScoped<IEventPublisher, PulsarEventPublisher>();

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