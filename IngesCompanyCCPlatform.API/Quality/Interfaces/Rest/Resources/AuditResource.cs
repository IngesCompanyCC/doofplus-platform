namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Response resource representing an audit with its findings.
/// </summary>
public record AuditResource(int Id, string Type, DateOnly ScheduledDate, string Status, string? EvidencePackageUrl, IEnumerable<AuditFindingResource> Findings);
