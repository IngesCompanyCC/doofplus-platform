namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query of the audit findings, optionally filtered by audit code.
/// </summary>
public record GetAllAuditFindingsQuery(string? AuditCode = null);
