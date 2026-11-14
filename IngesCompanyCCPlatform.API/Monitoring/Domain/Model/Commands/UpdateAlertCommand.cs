using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;

/// <summary>
///     Command to record the handling of an alert: who took charge, the response and the linked record.
/// </summary>
public record UpdateAlertCommand(
    AlertId AlertId,
    EAlertStatus Status,
    string AcknowledgedBy,
    string ResponseNote,
    string LinkedRecord);
