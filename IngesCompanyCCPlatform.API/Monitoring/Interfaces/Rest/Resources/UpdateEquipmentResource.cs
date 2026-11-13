namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;

/// <summary>
///     New data of a piece of equipment; its code does not change.
/// </summary>
public record UpdateEquipmentResource(
    string Name,
    string? Model,
    string? Location,
    string? CalibrationStatus,
    DateOnly? CalibrationDue,
    string? Certificate,
    DateOnly? NextMaintenance,
    string? MaintenanceOwner,
    string? WorkOrder,
    string? HealthNote);
