namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A traceability gap, as the API returns it.
/// </summary>
public record TraceabilityGapResource(
    int Id,
    int BatchId,
    string Description,
    string MissingReference,
    string AffectedProcess,
    string Status,
    int? ReviewedBy,
    DateTime DetectedAt,
    DateTime? ResolvedAt);
