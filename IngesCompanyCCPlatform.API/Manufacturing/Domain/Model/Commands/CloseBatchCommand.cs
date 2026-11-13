using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to close a production batch with the manufactured quantity.
/// </summary>
public record CloseBatchCommand(
    int BatchId,
    Quantity ManufacturedQuantity)
{
    public CloseBatchCommand(int batchId, decimal manufacturedQuantityValue, string manufacturedQuantityUnit)
        : this(batchId, new Quantity(manufacturedQuantityValue, manufacturedQuantityUnit))
    {
    }
}
