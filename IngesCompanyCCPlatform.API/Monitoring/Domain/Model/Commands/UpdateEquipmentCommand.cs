using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;

/// <summary>
///     Command to update a piece of equipment; its code does not change.
/// </summary>
public record UpdateEquipmentCommand(
    EquipmentId EquipmentId,
    string Name,
    string Model,
    string Location,
    ECalibrationStatus CalibrationStatus,
    DateOnly? CalibrationDue,
    string Certificate,
    DateOnly? NextMaintenance,
    string MaintenanceOwner,
    string WorkOrder,
    string HealthNote);
