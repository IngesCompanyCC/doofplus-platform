namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get the audit trail of one record.
/// </summary>
public record GetAuditTrailEntriesByEntityQuery(string EntityType, string EntityId);
