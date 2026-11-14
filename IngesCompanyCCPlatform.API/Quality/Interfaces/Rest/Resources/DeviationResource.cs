namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A deviation, as the API returns it.
/// </summary>
public record DeviationResource(
    int Id,
    string Code,
    string Title,
    string Description,
    string Severity,
    int BatchId,
    int? AssignedTo,
    string Status,
    DateTime DetectedAt,
    DateTime? ClosedAt);
