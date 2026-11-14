namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;

/// <summary>
///     A telemetry reading.
/// </summary>
public record ReadingResource(
    int Id,
    string SensorCode,
    string BatchCode,
    decimal Value,
    DateTime RecordedAt,
    string Source);
