using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;

/// <summary>
///     Command to raise an alert by hand.
/// </summary>
public record CreateAlertCommand(
    AlertCode Code,
    string SensorCode,
    string EquipmentCode,
    EAlertSeverity Severity,
    string Title,
    string Detail,
    string BatchCode,
    string LinkedRecord);
