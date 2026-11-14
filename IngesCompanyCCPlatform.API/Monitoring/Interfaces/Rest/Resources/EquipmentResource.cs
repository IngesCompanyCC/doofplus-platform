namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;

/// <summary>
///     A piece of equipment, as the Web App shows it.
/// </summary>
public record EquipmentResource(
    int Id,
    string Code,
    string Name,
    string Model,
    string Location,
    string CalibrationStatus,
    DateOnly? CalibrationDue,
    string Certificate,
    DateOnly? NextMaintenance,
    string MaintenanceOwner,
    string WorkOrder,
    string HealthNote);
