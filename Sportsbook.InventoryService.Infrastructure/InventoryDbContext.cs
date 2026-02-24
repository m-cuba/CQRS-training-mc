using Microsoft.EntityFrameworkCore;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Infrastructure.Messaging;

namespace Sportsbook.InventoryService.Infrastructure
{
    public class InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : DbContext(options), IUnitOfWork
    {
        public DbSet<EventStoreMessage> EventStore => Set<EventStoreMessage>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventStoreMessage>(builder =>
            {
                builder.ToTable("EventStore");

                builder.HasKey(e => e.Id);

                builder.Property(e => e.StreamId)
                    .IsRequired()
                    .HasMaxLength(200);

                builder.Property(e => e.EventType)
                    .IsRequired()
                    .HasMaxLength(200);

                builder.Property(e => e.Payload)
                    .IsRequired();

                builder.Property(e => e.OccurredAt)
                    .IsRequired();

                builder.Property(e => e.Version)
                    .IsRequired();

                builder.HasIndex(e => e.StreamId);
                builder.HasIndex(e => new { e.StreamId, e.Version }).IsUnique();
            });
        }

        Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
            => SaveChangesAsync(cancellationToken);
    }
}
