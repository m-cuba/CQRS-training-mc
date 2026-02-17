using Microsoft.EntityFrameworkCore;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Core.InventoryContext;

namespace Sportsbook.InventoryService.Infrastructure
{
    public class ReadDbContext(DbContextOptions<ReadDbContext> options) : DbContext(options), IReadUnitOfWork
    {
        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
        public DbSet<StockMovement> StockMovements => Set<StockMovement>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryItem>(builder =>
            {
                builder.ToTable("ReadInventoryItems");

                builder.HasKey(i => i.Id);

                builder.Property(i => i.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                builder.Property(i => i.Quantity)
                    .HasConversion(
                        q => q.Value,
                        v => new Quantity(v))
                    .IsRequired();

                builder.Property(i => i.Sku)
                    .HasConversion(
                        sku => sku.Value,
                        value => new Sku(value))
                    .IsRequired()
                    .HasMaxLength(50);

                builder.HasIndex(i => i.Sku).IsUnique();
                builder.HasIndex(i => i.Quantity);
            });

            modelBuilder.Entity<StockMovement>(builder =>
            {
                builder.ToTable("StockMovements");

                builder.HasKey(a => a.Id);

                builder.Property(a => a.Sku)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(a => a.QuantityChange)
                    .IsRequired();

                builder.Property(a => a.OccurredAt)
                    .IsRequired();

                builder.HasIndex(a => a.Sku);
                builder.HasIndex(a => a.OccurredAt);
            });
        }

        Task IReadUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);
    }
}