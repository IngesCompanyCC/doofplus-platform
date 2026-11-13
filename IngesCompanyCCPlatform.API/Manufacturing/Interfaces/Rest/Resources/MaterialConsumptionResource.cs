namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Response representation of raw material consumption.
/// </summary>
public record MaterialConsumptionResource(
    int Id,
    int ProductionBatchId,
    int RawMaterialLotId,
    decimal QuantityValue,
    string QuantityUnit,
    DateTime ConsumedAt);
