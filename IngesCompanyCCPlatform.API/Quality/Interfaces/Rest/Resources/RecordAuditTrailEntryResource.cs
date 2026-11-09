namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Request resource to record a change in the audit trail. Action: Created, Updated, Deleted or Signed.
/// </summary>
public record RecordAuditTrailEntryResource(string EntityType, string EntityId, string Action, string? OldValue, string? NewValue, int ChangedBy, string? Reason);
