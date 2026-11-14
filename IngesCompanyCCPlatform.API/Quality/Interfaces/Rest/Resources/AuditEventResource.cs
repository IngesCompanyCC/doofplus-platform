namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A audit event, as the API returns it.
/// </summary>
public record AuditEventResource(
    int Id,
    string AggregateType,
    int AggregateId,
    string EventType,
    string Description,
    string PerformedBy,
    DateTime OccurredAt);
