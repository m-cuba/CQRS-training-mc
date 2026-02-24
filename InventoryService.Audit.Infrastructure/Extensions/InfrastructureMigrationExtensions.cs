using InventoryService.Audit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryService.Audit.Infrastructure.Extensions
{
    public static class InfrastructureMigrationExtensions
    {
        public static void ApplyMigrations(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider
                .GetRequiredService<AuditDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
