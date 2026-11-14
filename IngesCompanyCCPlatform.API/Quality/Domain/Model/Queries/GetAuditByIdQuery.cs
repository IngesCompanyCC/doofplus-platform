namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a audit by its id.
/// </summary>
/// <param name="AuditId">The id of the audit.</param>
public record GetAuditByIdQuery(int AuditId);
