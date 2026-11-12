using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Entities;
using IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Transform;

public static class ProductionBatchResourceFromEntityAssembler
{
    public static ProductionBatchResource ToResourceFromEntity(ProductionBatch entity)
    {
        return new ProductionBatchResource(
            entity.Id,
            entity.BatchNumber.Value,
            entity.ProductionOrderId,
            entity.Status.ToString(),
            entity.PlannedQuantity.Value,
            entity.PlannedQuantity.Unit,
            entity.ManufacturedQuantity?.Value,
            entity.ManufacturedQuantity?.Unit,
            entity.StartedAt,
            entity.FinishedAt,
            entity.Incidents.Select(ToIncidentResource),
            entity.Consumptions.Select(ToConsumptionResource),
            entity.Events.Select(ToEventResource));
    }

    public static IncidentResource ToIncidentResource(Incident entity)
    {
        return new IncidentResource(
            entity.Id,
            entity.ProductionBatchId,
            entity.Description,
            entity.Severity.ToString(),
            entity.Status.ToString(),
            entity.RecordedAt);
    }

    public static MaterialConsumptionResource ToConsumptionResource(MaterialConsumption entity)
    {
        return new MaterialConsumptionResource(
            entity.Id,
            entity.ProductionBatchId,
            entity.RawMaterialLotId,
            entity.ConsumedQuantity.Value,
            entity.ConsumedQuantity.Unit,
            entity.ConsumedAt);
    }

    public static BatchEventResource ToEventResource(BatchEvent entity)
    {
        return new BatchEventResource(
            entity.Id,
            entity.ProductionBatchId,
            entity.Type,
            entity.Description,
            entity.PerformedBy,
            entity.OccurredAt);
    }
}
