namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     A production order with its operations, as the Web App shows it.
/// </summary>
public record ProductionOrderResource(
    int Id,
    string Code,
    string ProductCode,
    int FormulaId,
    string BatchCode,
    decimal PlannedQuantity,
    string Line,
    string Status,
    DateTime? PlannedStart,
    DateTime? PlannedEnd,
    IEnumerable<OperationResource> Operations);
