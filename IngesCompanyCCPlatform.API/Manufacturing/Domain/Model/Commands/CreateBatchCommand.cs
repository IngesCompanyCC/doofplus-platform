using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to register a new production batch.
/// </summary>
public record CreateBatchCommand(
    BatchNumber BatchNumber,
    int ProductionOrderId,
    Quantity PlannedQuantity,
    string? Line = null,
    string? Owner = null)
{
    public CreateBatchCommand(
        string batchNumber,
        int productionOrderId,
        decimal quantityValue,
        string quantityUnit,
        string? line = null,
        string? owner = null)
        : this(new BatchNumber(batchNumber), productionOrderId, new Quantity(quantityValue, quantityUnit), line, owner)
    {
    }
}
