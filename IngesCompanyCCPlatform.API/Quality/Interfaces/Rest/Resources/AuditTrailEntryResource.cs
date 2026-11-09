namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Response resource representing an entry of the audit trail.
/// </summary>
public record AuditTrailEntryResource(int Id, string EntityType, string EntityId, string Action, string? OldValue, string? NewValue, int ChangedBy, DateTime ChangedAt, string? Reason);
