namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;

/// <summary>
///     A sensor, as the Web App shows it.
/// </summary>
public record SensorResource(
    int Id,
    string Code,
    string DeviceId,
    string Type,
    string Unit,
    string EquipmentCode,
    decimal LowerLimit,
    decimal UpperLimit,
    string Status,
    string AssignedBatch);
