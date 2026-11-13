using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;

/// <summary>
///     Command to register a sensor and its ThingsBoard device.
/// </summary>
public record CreateSensorCommand(
    SensorCode Code,
    DeviceId DeviceId,
    ESensorType Type,
    string Unit,
    EquipmentCode EquipmentCode,
    decimal LowerLimit,
    decimal UpperLimit,
    ESensorStatus Status,
    string AssignedBatch);
