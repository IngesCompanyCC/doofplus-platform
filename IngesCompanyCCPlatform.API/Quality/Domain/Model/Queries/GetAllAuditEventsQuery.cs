namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query of the audit events, optionally filtered by record.
/// </summary>
public record GetAllAuditEventsQuery(string? Record = null);
