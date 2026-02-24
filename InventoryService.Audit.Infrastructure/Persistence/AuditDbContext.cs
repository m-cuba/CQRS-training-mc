using InventoryService.Audit.Application.Seedwork.Interfaces;
using InventoryService.Audit.Core;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Audit.Infrastructure.Persistence
{
    public class AuditDbContext(DbContextOptions<AuditDbContext> options) : DbContext(options), IUnitOfWork
    {
        public DbSet<AuditStockEntry> AuditEntries => Set<AuditStockEntry>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditStockEntry>(builder =>
            {
                builder.ToTable("AuditEntries");

                builder.HasKey(x => x.MovementId);

                builder.Property(x => x.Sku).IsRequired().HasMaxLength(50);
                builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
                builder.Property(x => x.QuantityDelta).IsRequired();
                builder.Property(x => x.OccurredOn).IsRequired();

                builder.HasIndex(x => x.Sku);
                builder.HasIndex(x => x.OccurredOn);
            });
        }

        Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            return SaveChangesAsync(cancellationToken);
        }
    }
}
