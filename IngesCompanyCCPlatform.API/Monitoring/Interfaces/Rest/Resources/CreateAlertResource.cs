namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;

/// <summary>
///     Data to raise an alert by hand. <c>severity</c> is Warning or Critical.
/// </summary>
public record CreateAlertResource(
    string Code,
    string? SensorCode,
    string? EquipmentCode,
    string Severity,
    string Title,
    string? Detail,
    string? BatchCode,
    string? LinkedRecord);
