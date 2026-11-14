namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;

/// <summary>
///     An alert, as the Web App shows it.
/// </summary>
public record AlertResource(
    int Id,
    string Code,
    string SensorCode,
    string EquipmentCode,
    string Severity,
    string Status,
    string Title,
    string Detail,
    string BatchCode,
    string AcknowledgedBy,
    DateTime? AcknowledgedAt,
    string ResponseNote,
    string LinkedRecord);
