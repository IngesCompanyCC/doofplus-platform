using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Entities;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Manufacturing.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Entity Framework Core mapping of the catalog of Manufacturing: products, master formulas with their lines,
///     production orders with their operations, and lots of raw material.
/// </summary>
/// <remarks>
///     The tables are <c>products</c>, <c>master_formulas</c>, <c>formula_items</c>, <c>production_orders</c>,
///     <c>operations</c> and <c>raw_material_lots</c>. The production batches keep their own mapping in
///     <see cref="ModelBuilderExtensions" />.
/// </remarks>
public static class CatalogModelBuilderExtensions
{
    /// <summary>
    ///     Applies the mapping of the catalog of Manufacturing.
    /// </summary>
    /// <param name="builder">The model builder of the database context.</param>
    public static void ApplyManufacturingCatalogConfiguration(this ModelBuilder builder)
    {
        // Products
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Product>().Property(p => p.Id)
            .HasConversion(new StronglyTypedIdConverter<ProductId, int>(id => id.Value, value => new ProductId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Product>().Property(p => p.Code)
            .HasConversion(code => code.Value, value => new ProductCode(value))
            .IsRequired()
            .HasMaxLength(30);
        builder.Entity<Product>().HasIndex(p => p.Code).IsUnique();
        builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(150);
        builder.Entity<Product>().Property(p => p.DosageForm).HasConversion<string>().HasMaxLength(16).IsRequired();

        // Master formulas and their lines
        builder.Entity<MasterFormula>().HasKey(f => f.Id);
        builder.Entity<MasterFormula>().Property(f => f.Id)
            .HasConversion(new StronglyTypedIdConverter<MasterFormulaId, int>(id => id.Value, value => new MasterFormulaId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<MasterFormula>().Property(f => f.ProductCode)
            .HasConversion(code => code.Value, value => new ProductCode(value))
            .IsRequired()
            .HasMaxLength(30);
        builder.Entity<MasterFormula>().Property(f => f.Version).IsRequired().HasMaxLength(20);
        builder.Entity<MasterFormula>().HasIndex(f => new { f.ProductCode, f.Version }).IsUnique();
        builder.Entity<MasterFormula>().Property(f => f.Status).HasConversion<string>().HasMaxLength(16).IsRequired();
        builder.Entity<MasterFormula>().Property(f => f.ApprovedBy).IsRequired().HasMaxLength(100);
        builder.Entity<MasterFormula>().Property(f => f.ApprovedAt);
        builder.Entity<MasterFormula>().Property(f => f.BatchSize).HasPrecision(12, 4).IsRequired();
        builder.Entity<MasterFormula>().Ignore(f => f.IsApproved);
        builder.Entity<MasterFormula>()
            .HasMany(f => f.Items)
            .WithOne()
            .HasForeignKey(i => i.MasterFormulaId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<MasterFormula>().Navigation(f => f.Items).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Entity<FormulaItem>().HasKey(i => i.Id);
        builder.Entity<FormulaItem>().Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<FormulaItem>().Property(i => i.MasterFormulaId)
            .HasConversion(new StronglyTypedIdConverter<MasterFormulaId, int>(id => id.Value, value => new MasterFormulaId(value)))
            .IsRequired();
        builder.Entity<FormulaItem>().Property(i => i.Kind).HasConversion<string>().HasMaxLength(16).IsRequired();
        builder.Entity<FormulaItem>().Property(i => i.Name).IsRequired().HasMaxLength(150);
        builder.Entity<FormulaItem>().Property(i => i.Role).IsRequired().HasMaxLength(60);
        builder.Entity<FormulaItem>().Property(i => i.Target).IsRequired().HasMaxLength(60);
        builder.Entity<FormulaItem>().Property(i => i.Tolerance).IsRequired().HasMaxLength(60);
        builder.Entity<FormulaItem>().Property(i => i.Control).IsRequired().HasMaxLength(150);

        // Production orders and their operations
        builder.Entity<ProductionOrder>().HasKey(o => o.Id);
        builder.Entity<ProductionOrder>().Property(o => o.Id)
            .HasConversion(new StronglyTypedIdConverter<ProductionOrderId, int>(id => id.Value, value => new ProductionOrderId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<ProductionOrder>().Property(o => o.Code)
            .HasConversion(code => code.Value, value => new ProductionOrderCode(value))
            .IsRequired()
            .HasMaxLength(30);
        builder.Entity<ProductionOrder>().HasIndex(o => o.Code).IsUnique();
        builder.Entity<ProductionOrder>().Property(o => o.ProductCode)
            .HasConversion(code => code.Value, value => new ProductCode(value))
            .IsRequired()
            .HasMaxLength(30);
        builder.Entity<ProductionOrder>().Property(o => o.MasterFormulaId)
            .HasConversion(new StronglyTypedIdConverter<MasterFormulaId, int>(id => id.Value, value => new MasterFormulaId(value)))
            .IsRequired();
        builder.Entity<ProductionOrder>().Property(o => o.BatchCode).IsRequired().HasMaxLength(30);
        builder.Entity<ProductionOrder>().Property(o => o.PlannedQuantity).HasPrecision(12, 4).IsRequired();
        builder.Entity<ProductionOrder>().Property(o => o.Line).IsRequired().HasMaxLength(30);
        builder.Entity<ProductionOrder>().Property(o => o.Status).HasConversion<string>().HasMaxLength(16).IsRequired();
        builder.Entity<ProductionOrder>().Property(o => o.PlannedStart);
        builder.Entity<ProductionOrder>().Property(o => o.PlannedEnd);
        builder.Entity<ProductionOrder>()
            .HasMany(o => o.Operations)
            .WithOne()
            .HasForeignKey(op => op.ProductionOrderId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<ProductionOrder>().Navigation(o => o.Operations).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Entity<Operation>().HasKey(op => op.Id);
        builder.Entity<Operation>().Property(op => op.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Operation>().Property(op => op.ProductionOrderId)
            .HasConversion(new StronglyTypedIdConverter<ProductionOrderId, int>(id => id.Value, value => new ProductionOrderId(value)))
            .IsRequired();
        builder.Entity<Operation>().Property(op => op.Sequence).IsRequired();
        builder.Entity<Operation>().HasIndex(op => new { op.ProductionOrderId, op.Sequence }).IsUnique();
        builder.Entity<Operation>().Property(op => op.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Operation>().Property(op => op.Equipment).IsRequired().HasMaxLength(100);
        builder.Entity<Operation>().Property(op => op.Progress).IsRequired();
        builder.Entity<Operation>().Property(op => op.Status).HasConversion<string>().HasMaxLength(16).IsRequired();
        builder.Entity<Operation>().Property(op => op.Reference).IsRequired().HasMaxLength(30);

        // Lots of raw material
        builder.Entity<RawMaterialLot>().HasKey(l => l.Id);
        builder.Entity<RawMaterialLot>().Property(l => l.Id)
            .HasConversion(new StronglyTypedIdConverter<RawMaterialLotId, int>(id => id.Value, value => new RawMaterialLotId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<RawMaterialLot>().Property(l => l.Code)
            .HasConversion(code => code.Value, value => new RawMaterialLotCode(value))
            .IsRequired()
            .HasMaxLength(30);
        builder.Entity<RawMaterialLot>().HasIndex(l => l.Code).IsUnique();
        builder.Entity<RawMaterialLot>().Property(l => l.ReceiptCode).IsRequired().HasMaxLength(30);
        builder.Entity<RawMaterialLot>().Property(l => l.Material).IsRequired().HasMaxLength(150);
        builder.Entity<RawMaterialLot>().Property(l => l.Category).HasConversion<string>().HasMaxLength(16).IsRequired();
        builder.Entity<RawMaterialLot>().Property(l => l.Supplier).IsRequired().HasMaxLength(150);
        builder.Entity<RawMaterialLot>().Property(l => l.SupplierLot).IsRequired().HasMaxLength(50);
        builder.Entity<RawMaterialLot>().Property(l => l.Quantity).IsRequired().HasMaxLength(100);
        builder.Entity<RawMaterialLot>().Property(l => l.ExpiryDate).IsRequired();
        builder.Entity<RawMaterialLot>().Property(l => l.StorageLocation).IsRequired().HasMaxLength(100);
        builder.Entity<RawMaterialLot>().Property(l => l.Certificate).IsRequired().HasMaxLength(150);
        builder.Entity<RawMaterialLot>().Property(l => l.Status).HasConversion<string>().HasMaxLength(16).IsRequired();
        builder.Entity<RawMaterialLot>().Property(l => l.PackagingVerified).IsRequired();
        builder.Entity<RawMaterialLot>().Property(l => l.IdentityVerified).IsRequired();
        builder.Entity<RawMaterialLot>().Property(l => l.SamplingStatus).HasConversion<string>().HasMaxLength(16).IsRequired();
        builder.Entity<RawMaterialLot>().Property(l => l.AllocatedBatch).IsRequired().HasMaxLength(30);
    }
}
