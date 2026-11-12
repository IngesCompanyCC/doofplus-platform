namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Response representation of a production incident.
/// </summary>
public record IncidentResource(
    int Id,
    int ProductionBatchId,
    string Description,
    string Severity,
    string Status,
    DateTime RecordedAt);
