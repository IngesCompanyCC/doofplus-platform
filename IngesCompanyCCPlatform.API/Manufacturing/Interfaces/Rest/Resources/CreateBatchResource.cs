namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Request payload to create a new production batch.
/// </summary>
public record CreateBatchResource(
    string BatchNumber,
    int ProductionOrderId,
    decimal PlannedQuantityValue,
    string PlannedQuantityUnit,
    string? Line = null,
    string? Owner = null);
