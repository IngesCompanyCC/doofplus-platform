using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Manufacturing.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the Manufacturing bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    public static void ApplyManufacturingConfiguration(this ModelBuilder builder)
    {
        // 1. ProductionBatch Aggregate Root
        builder.Entity<ProductionBatch>().ToTable("production_batches");
        builder.Entity<ProductionBatch>().HasKey(b => b.Id);
        builder.Entity<ProductionBatch>().Property(b => b.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ProductionBatch>().Property(b => b.ProductionOrderId).IsRequired();
        builder.Entity<ProductionBatch>().Property(b => b.Status).IsRequired().HasConversion<string>().HasMaxLength(50);
        builder.Entity<ProductionBatch>().Property(b => b.StartedAt);
        builder.Entity<ProductionBatch>().Property(b => b.FinishedAt);

        // Value Object: BatchNumber
        builder.Entity<ProductionBatch>().OwnsOne(b => b.BatchNumber, bn =>
        {
            bn.WithOwner().HasForeignKey("Id");
            bn.Property(p => p.Value).HasColumnName("batch_number").HasMaxLength(30).IsRequired();
            bn.HasIndex(p => p.Value).IsUnique();
        });

        // Value Object: PlannedQuantity
        builder.Entity<ProductionBatch>().OwnsOne(b => b.PlannedQuantity, pq =>
        {
            pq.WithOwner().HasForeignKey("Id");
            pq.Property(p => p.Value).HasColumnName("planned_quantity_value").HasPrecision(18, 2).IsRequired();
            pq.Property(p => p.Unit).HasColumnName("planned_quantity_unit").HasMaxLength(20).IsRequired();
        });

        // Value Object: ManufacturedQuantity (Nullable)
        builder.Entity<ProductionBatch>().OwnsOne(b => b.ManufacturedQuantity, mq =>
        {
            mq.WithOwner().HasForeignKey("Id");
            mq.Property(p => p.Value).HasColumnName("manufactured_quantity_value").HasPrecision(18, 2);
            mq.Property(p => p.Unit).HasColumnName("manufactured_quantity_unit").HasMaxLength(20);
        });

        // Navigation Collections
        builder.Entity<ProductionBatch>()
            .HasMany(b => b.Incidents)
            .WithOne()
            .HasForeignKey(i => i.ProductionBatchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ProductionBatch>()
            .HasMany(b => b.Consumptions)
            .WithOne()
            .HasForeignKey(m => m.ProductionBatchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ProductionBatch>()
            .HasMany(b => b.Events)
            .WithOne()
            .HasForeignKey(e => e.ProductionBatchId)
            .OnDelete(DeleteBehavior.Cascade);

        // 2. Incident Entity
        builder.Entity<Incident>().ToTable("incidents");
        builder.Entity<Incident>().HasKey(i => i.Id);
        builder.Entity<Incident>().Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Incident>().Property(i => i.ProductionBatchId).IsRequired();
        builder.Entity<Incident>().Property(i => i.Description).IsRequired().HasMaxLength(500);
        builder.Entity<Incident>().Property(i => i.Severity).IsRequired().HasConversion<string>().HasMaxLength(50);
        builder.Entity<Incident>().Property(i => i.Status).IsRequired().HasConversion<string>().HasMaxLength(50);
        builder.Entity<Incident>().Property(i => i.RecordedAt).IsRequired();

        // 3. MaterialConsumption Entity
        builder.Entity<MaterialConsumption>().ToTable("material_consumptions");
        builder.Entity<MaterialConsumption>().HasKey(m => m.Id);
        builder.Entity<MaterialConsumption>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<MaterialConsumption>().Property(m => m.ProductionBatchId).IsRequired();
        builder.Entity<MaterialConsumption>().Property(m => m.RawMaterialLotId).IsRequired();
        builder.Entity<MaterialConsumption>().Property(m => m.ConsumedAt).IsRequired();

        builder.Entity<MaterialConsumption>().OwnsOne(m => m.ConsumedQuantity, cq =>
        {
            cq.WithOwner().HasForeignKey("Id");
            cq.Property(p => p.Value).HasColumnName("consumed_quantity_value").HasPrecision(18, 2).IsRequired();
            cq.Property(p => p.Unit).HasColumnName("consumed_quantity_unit").HasMaxLength(20).IsRequired();
        });

        // 4. BatchEvent Entity
        builder.Entity<BatchEvent>().ToTable("batch_events");
        builder.Entity<BatchEvent>().HasKey(e => e.Id);
        builder.Entity<BatchEvent>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<BatchEvent>().Property(e => e.ProductionBatchId).IsRequired();
        builder.Entity<BatchEvent>().Property(e => e.Type).IsRequired().HasMaxLength(100);
        builder.Entity<BatchEvent>().Property(e => e.Description).IsRequired().HasMaxLength(500);
        builder.Entity<BatchEvent>().Property(e => e.OccurredAt).IsRequired();
        builder.Entity<BatchEvent>().Property(e => e.PerformedBy);
    }
}
