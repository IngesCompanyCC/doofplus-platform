namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Response representation of a production batch aggregate.
/// </summary>
public record ProductionBatchResource(
    int Id,
    string BatchNumber,
    int ProductionOrderId,
    string Status,
    decimal PlannedQuantityValue,
    string PlannedQuantityUnit,
    string? Line,
    string? Owner,
    int Progress,
    decimal? ManufacturedQuantityValue,
    string? ManufacturedQuantityUnit,
    DateTime? StartedAt,
    DateTime? FinishedAt,
    IEnumerable<IncidentResource> Incidents,
    IEnumerable<MaterialConsumptionResource> Consumptions,
    IEnumerable<BatchEventResource> Events);
