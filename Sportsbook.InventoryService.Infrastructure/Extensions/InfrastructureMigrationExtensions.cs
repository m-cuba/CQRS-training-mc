using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Sportsbook.InventoryService.Infrastructure.Extensions
{
    public static class InfrastructureMigrationExtensions
    {
        public static void ApplyMigrations(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider
                .GetRequiredService<InventoryDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
