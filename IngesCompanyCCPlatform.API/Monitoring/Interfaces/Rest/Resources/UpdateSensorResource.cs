namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;

/// <summary>
///     New data of a sensor; its code and device do not change.
/// </summary>
public record UpdateSensorResource(
    string Type,
    string? Unit,
    string EquipmentCode,
    decimal LowerLimit,
    decimal UpperLimit,
    string? Status,
    string? AssignedBatch);
