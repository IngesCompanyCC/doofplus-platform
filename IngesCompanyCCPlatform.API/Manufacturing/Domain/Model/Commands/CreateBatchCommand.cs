using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to register a new production batch.
/// </summary>
public record CreateBatchCommand(
    BatchNumber BatchNumber,
    int ProductionOrderId,
    Quantity PlannedQuantity)
{
    public CreateBatchCommand(
        string batchNumber,
        int productionOrderId,
        decimal quantityValue,
        string quantityUnit)
        : this(new BatchNumber(batchNumber), productionOrderId, new Quantity(quantityValue, quantityUnit))
    {
    }
}
