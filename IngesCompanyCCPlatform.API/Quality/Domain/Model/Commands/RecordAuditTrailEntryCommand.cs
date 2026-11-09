using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to record a change in the audit trail (insert only).
/// </summary>
public record RecordAuditTrailEntryCommand(string EntityType, string EntityId, EAuditAction Action, string? OldValue, string? NewValue, UserId ChangedBy, string? Reason);
