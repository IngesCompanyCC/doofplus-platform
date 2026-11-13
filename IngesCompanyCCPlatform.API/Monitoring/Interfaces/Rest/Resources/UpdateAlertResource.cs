namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;

/// <summary>
///     Handling of an alert. <c>status</c> is Open, Acknowledged or UnderInvestigation (also under-investigation).
/// </summary>
public record UpdateAlertResource(
    string Status,
    string? AcknowledgedBy,
    string? ResponseNote,
    string? LinkedRecord);
