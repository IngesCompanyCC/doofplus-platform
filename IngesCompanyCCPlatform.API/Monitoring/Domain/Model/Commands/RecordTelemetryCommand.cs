using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;

/// <summary>
///     Command with a reading sent by ThingsBoard for one of its devices (TS04).
/// </summary>
public record RecordTelemetryCommand(
    DeviceId DeviceId,
    decimal Value,
    DateTime? RecordedAt);
