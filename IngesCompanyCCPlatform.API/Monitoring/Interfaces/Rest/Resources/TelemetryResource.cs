namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;

/// <summary>
///     Payload that ThingsBoard sends for a device (TS04).
/// </summary>
public record TelemetryResource(
    string DeviceId,
    decimal Value,
    DateTime? RecordedAt);
