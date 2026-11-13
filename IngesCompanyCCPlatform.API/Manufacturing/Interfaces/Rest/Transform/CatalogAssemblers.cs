using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of the catalog of Manufacturing from the resources. Invalid codes and enums throw the key
///     of their message, which the global exception handler answers with a 400.
/// </summary>
public static class CatalogCommandFromResourceAssembler
{
    /// <summary>Builds the command to register a product.</summary>
    public static CreateProductCommand ToCommandFromResource(CreateProductResource resource)
    {
        return new CreateProductCommand(new ProductCode(resource.Code), resource.Name,
            EnumParser.Parse<EDosageForm>(resource.DosageForm, "manufacturing.product.error.dosageForm.invalid"));
    }

    /// <summary>Builds the command to update a product.</summary>
    public static UpdateProductCommand ToCommandFromResource(int productId, UpdateProductResource resource)
    {
        return new UpdateProductCommand(productId, resource.Name,
            EnumParser.Parse<EDosageForm>(resource.DosageForm, "manufacturing.product.error.dosageForm.invalid"));
    }

    /// <summary>Builds the command to write a new version of a master formula.</summary>
    public static CreateMasterFormulaCommand ToCommandFromResource(CreateMasterFormulaResource resource)
    {
        var items = (resource.Items ?? []).Select(item => new FormulaItemData(
            EnumParser.Parse<EFormulaItemKind>(item.Kind, "manufacturing.formula.error.itemKind.invalid"),
            item.Name, item.Role ?? string.Empty, item.Target ?? string.Empty, item.Tolerance ?? string.Empty,
            item.Control ?? string.Empty)).ToList();
        return new CreateMasterFormulaCommand(new ProductCode(resource.ProductCode), resource.Version,
            resource.BatchSize, items);
    }

    /// <summary>Builds the command to plan a production order.</summary>
    public static CreateProductionOrderCommand ToCommandFromResource(CreateProductionOrderResource resource)
    {
        var operations = (resource.Operations ?? [])
            .Select(operation => new OperationData(operation.Sequence, operation.Name, operation.Equipment ?? string.Empty))
            .ToList();
        return new CreateProductionOrderCommand(new ProductionOrderCode(resource.Code), resource.FormulaId,
            resource.BatchCode ?? string.Empty, resource.PlannedQuantity, resource.Line ?? string.Empty,
            resource.PlannedStart, resource.PlannedEnd, operations);
    }

    /// <summary>Builds the command to change the status of a production order.</summary>
    public static ChangeProductionOrderStatusCommand ToCommandFromResource(int orderId,
        ChangeProductionOrderStatusResource resource)
    {
        return new ChangeProductionOrderStatusCommand(orderId,
            EnumParser.Parse<EProductionOrderStatus>(resource.Status, "manufacturing.order.error.status.invalid"));
    }

    /// <summary>Builds the command to record the progress of an operation.</summary>
    public static UpdateOperationCommand ToCommandFromResource(int orderId, int sequence,
        UpdateOperationResource resource)
    {
        return new UpdateOperationCommand(orderId, sequence, resource.Progress,
            EnumParser.Parse<EOperationStatus>(resource.Status, "manufacturing.order.error.operationStatus.invalid"),
            resource.Reference ?? string.Empty);
    }

    /// <summary>Builds the command to receive a lot of raw material.</summary>
    public static ReceiveRawMaterialLotCommand ToCommandFromResource(ReceiveRawMaterialLotResource resource)
    {
        return new ReceiveRawMaterialLotCommand(new RawMaterialLotCode(resource.Code),
            resource.ReceiptCode ?? string.Empty, resource.Material,
            EnumParser.Parse<ERawMaterialCategory>(resource.Category, "manufacturing.lot.error.category.invalid"),
            resource.Supplier ?? string.Empty, resource.SupplierLot ?? string.Empty, resource.Quantity ?? string.Empty,
            resource.ExpiryDate, resource.StorageLocation ?? string.Empty, resource.Certificate ?? string.Empty);
    }

    /// <summary>Builds the command to record the inspection of a lot.</summary>
    public static InspectRawMaterialLotCommand ToCommandFromResource(int lotId, InspectRawMaterialLotResource resource)
    {
        return new InspectRawMaterialLotCommand(lotId, resource.PackagingVerified, resource.IdentityVerified,
            EnumParser.Parse<ESamplingStatus>(resource.SamplingStatus, "manufacturing.lot.error.samplingStatus.invalid"),
            resource.StorageLocation ?? string.Empty);
    }
}

/// <summary>
///     Builds the resources of the catalog of Manufacturing from the aggregates.
/// </summary>
public static class CatalogResourceFromEntityAssembler
{
    /// <summary>Builds the resource of a product.</summary>
    public static ProductResource ToResourceFromEntity(Product entity)
    {
        return new ProductResource(entity.Id.Value, entity.Code.Value, entity.Name, entity.DosageForm.ToString());
    }

    /// <summary>Builds the resource of a master formula with its lines.</summary>
    public static MasterFormulaResource ToResourceFromEntity(MasterFormula entity)
    {
        return new MasterFormulaResource(entity.Id.Value, entity.ProductCode.Value, entity.Version,
            entity.Status.ToString(), entity.ApprovedBy, entity.ApprovedAt, entity.BatchSize,
            entity.Items.Select(item => new FormulaItemResource(item.Id, item.Kind.ToString(), item.Name, item.Role,
                item.Target, item.Tolerance, item.Control)));
    }

    /// <summary>Builds the resource of a production order with its operations.</summary>
    public static ProductionOrderResource ToResourceFromEntity(ProductionOrder entity)
    {
        return new ProductionOrderResource(entity.Id.Value, entity.Code.Value, entity.ProductCode.Value,
            entity.MasterFormulaId.Value, entity.BatchCode, entity.PlannedQuantity, entity.Line,
            entity.Status.ToString(), AsUtc(entity.PlannedStart), AsUtc(entity.PlannedEnd),
            entity.Operations.Select(operation => new OperationResource(operation.Sequence, operation.Name,
                operation.Equipment, operation.Progress, operation.Status.ToString(), operation.Reference)));
    }

    /// <summary>Builds the resource of a lot of raw material.</summary>
    public static RawMaterialLotResource ToResourceFromEntity(RawMaterialLot entity)
    {
        return new RawMaterialLotResource(entity.Id.Value, entity.Code.Value, entity.ReceiptCode, entity.Material,
            entity.Category.ToString(), entity.Supplier, entity.SupplierLot, entity.Quantity, entity.ExpiryDate,
            entity.StorageLocation, entity.Certificate, entity.Status.ToString(), entity.PackagingVerified,
            entity.IdentityVerified, entity.SamplingStatus.ToString(), entity.AllocatedBatch);
    }

    private static DateTime? AsUtc(DateTime? value)
    {
        return value is null ? null : DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
    }
}
