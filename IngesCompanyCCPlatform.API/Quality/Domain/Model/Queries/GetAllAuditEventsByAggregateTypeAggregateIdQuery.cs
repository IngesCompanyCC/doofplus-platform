namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get the audit events filtered by aggregate type and aggregate id.
/// </summary>
public record GetAllAuditEventsByAggregateTypeAggregateIdQuery(string? AggregateType, int? AggregateId);
