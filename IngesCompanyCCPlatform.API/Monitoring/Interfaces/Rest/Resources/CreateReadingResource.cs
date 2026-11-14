namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;

/// <summary>
///     Data to record a reading by hand. Without <c>batchCode</c> it takes the batch the sensor is monitoring.
/// </summary>
public record CreateReadingResource(
    string SensorCode,
    string? BatchCode,
    decimal Value,
    DateTime? RecordedAt,
    string? Source);
