namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Data to plan a production order that follows an approved formula.
/// </summary>
public record CreateProductionOrderResource(
    string Code,
    int FormulaId,
    string? BatchCode,
    decimal PlannedQuantity,
    string? Line,
    DateTime? PlannedStart,
    DateTime? PlannedEnd,
    IReadOnlyList<CreateOperationResource>? Operations);
