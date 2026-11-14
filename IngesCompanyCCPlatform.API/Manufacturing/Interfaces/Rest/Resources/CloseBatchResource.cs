namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Request payload to close a production batch.
/// </summary>
public record CloseBatchResource(
    decimal ManufacturedQuantityValue,
    string ManufacturedQuantityUnit);
