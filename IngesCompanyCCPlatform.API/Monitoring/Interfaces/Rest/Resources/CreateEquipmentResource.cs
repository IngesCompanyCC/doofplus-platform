namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;

/// <summary>
///     Data to register a piece of equipment. <c>calibrationStatus</c> is Fit, Due or NotFit (also fit, due or not-fit); Fit when omitted.
/// </summary>
public record CreateEquipmentResource(
    string Code,
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
