namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;

/// <summary>
///     Data to register a sensor. <c>type</c> is Temperature, Humidity or Pressure; <c>status</c> is Active (default) or Offline.
/// </summary>
public record CreateSensorResource(
    string Code,
    string DeviceId,
    string Type,
    string? Unit,
    string EquipmentCode,
    decimal LowerLimit,
    decimal UpperLimit,
    string? Status,
    string? AssignedBatch);
