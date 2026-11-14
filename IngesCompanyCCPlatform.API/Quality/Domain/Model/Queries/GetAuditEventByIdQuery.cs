namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a audit event by its id.
/// </summary>
/// <param name="AuditEventId">The id of the audit event.</param>
public record GetAuditEventByIdQuery(int AuditEventId);
