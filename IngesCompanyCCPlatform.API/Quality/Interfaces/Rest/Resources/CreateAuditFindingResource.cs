namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a audit finding.
/// </summary>
public record CreateAuditFindingResource(
    int AuditId,
    string Title,
    string? Description,
    string Severity,
    int? RelatedDeviationId);
