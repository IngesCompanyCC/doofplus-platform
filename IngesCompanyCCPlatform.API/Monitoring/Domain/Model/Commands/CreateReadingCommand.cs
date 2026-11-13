using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;

/// <summary>
///     Command to record a reading of a sensor.
/// </summary>
public record CreateReadingCommand(
    SensorCode SensorCode,
    string BatchCode,
    decimal Value,
    DateTime? RecordedAt,
    string Source);
