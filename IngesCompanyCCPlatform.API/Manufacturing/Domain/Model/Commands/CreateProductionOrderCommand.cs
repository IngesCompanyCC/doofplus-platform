using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to plan a production order that follows an approved master formula.
/// </summary>
public record CreateProductionOrderCommand(
    ProductionOrderCode Code,
    MasterFormulaId MasterFormulaId,
    string BatchCode,
    decimal PlannedQuantity,
    string Line,
    DateTime? PlannedStart,
    DateTime? PlannedEnd,
    IReadOnlyList<OperationData> Operations);
