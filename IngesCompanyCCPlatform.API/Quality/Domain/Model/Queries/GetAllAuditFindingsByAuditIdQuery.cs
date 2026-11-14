namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get the audit findings filtered by audit id.
/// </summary>
public record GetAllAuditFindingsByAuditIdQuery(int AuditId);
