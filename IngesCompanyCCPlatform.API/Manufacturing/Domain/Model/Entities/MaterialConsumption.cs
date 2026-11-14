using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Entities;

/// <summary>
///     Represents material consumption recorded against a production batch.
/// </summary>
public class MaterialConsumption
{
    public int Id { get; private set; }
    public int ProductionBatchId { get; private set; }
    public int RawMaterialLotId { get; private set; }
    public Quantity ConsumedQuantity { get; private set; } = null!;
    public DateTime ConsumedAt { get; private set; }

    protected MaterialConsumption()
    {
    }

    public MaterialConsumption(int rawMaterialLotId, Quantity consumedQuantity)
    {
        if (rawMaterialLotId <= 0)
            throw new ArgumentException("Raw material lot ID must be positive.", nameof(rawMaterialLotId));

        RawMaterialLotId = rawMaterialLotId;
        ConsumedQuantity = consumedQuantity ?? throw new ArgumentNullException(nameof(consumedQuantity));
        ConsumedAt = DateTime.UtcNow;
    }
}
