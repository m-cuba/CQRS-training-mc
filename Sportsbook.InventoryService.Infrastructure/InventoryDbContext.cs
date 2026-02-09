using Microsoft.EntityFrameworkCore;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Core.InventoryContext;

namespace Sportsbook.InventoryService.Infrastructure
{
    public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options), IUnitOfWork
    {
        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryItem>(builder =>
            {
                builder.ToTable("InventoryItems");

                builder.HasKey(i => i.Id);

                builder.Property(i => i.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                builder.Property(i => i.Quantity)
                    .IsRequired();

                builder.Property(i => i.Sku)
                    .HasConversion(
                        sku => sku.Value,
                        value => new Sku(value))
                    .IsRequired()
                    .HasMaxLength(50);

                builder.HasIndex(i => i.Sku).IsUnique();
            });
        }

        Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);
    }
}
