namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A audit finding, as the API returns it.
/// </summary>
public record AuditFindingResource(
    int Id,
    int AuditId,
    string Title,
    string Description,
    string Severity,
    int? RelatedDeviationId,
    DateTime IdentifiedAt);
