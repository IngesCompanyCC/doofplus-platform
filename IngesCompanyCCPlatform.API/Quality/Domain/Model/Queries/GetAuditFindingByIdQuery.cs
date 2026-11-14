namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a audit finding by its id.
/// </summary>
/// <param name="AuditFindingId">The id of the audit finding.</param>
public record GetAuditFindingByIdQuery(int AuditFindingId);
