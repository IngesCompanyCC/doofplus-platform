using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;

/// <summary>
///     Command to update a sensor; its code and device do not change.
/// </summary>
public record UpdateSensorCommand(
    SensorId SensorId,
    ESensorType Type,
    string Unit,
    EquipmentCode EquipmentCode,
    decimal LowerLimit,
    decimal UpperLimit,
    ESensorStatus Status,
    string AssignedBatch);
