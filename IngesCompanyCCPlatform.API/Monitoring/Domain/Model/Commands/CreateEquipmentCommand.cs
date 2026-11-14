using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;

/// <summary>
///     Command to register a piece of equipment of the plant.
/// </summary>
public record CreateEquipmentCommand(
    EquipmentCode Code,
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
