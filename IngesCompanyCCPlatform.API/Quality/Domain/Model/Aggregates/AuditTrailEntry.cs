using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Represents the AuditTrailEntry aggregate root within the Quality &amp; Compliance bounded context.
/// </summary>
/// <remarks>
///     Insert only: an entry is never changed or deleted. It persists the "Audit trail" read model
///     of the Design-Level EventStorming, with the old and new values of each change as JSON.
/// </remarks>
public class AuditTrailEntry : AggregateRoot
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AuditTrailEntry" /> aggregate. Used by Entity Framework Core.
    /// </summary>
    protected AuditTrailEntry()
    {
        EntityType = string.Empty;
        EntityId = string.Empty;
    }

    /// <summary>
    ///     Initializes a new entry of the audit trail.
    /// </summary>
    /// <param name="command">The <see cref="RecordAuditTrailEntryCommand" /> with the change.</param>
    public AuditTrailEntry(RecordAuditTrailEntryCommand command) : this()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(command.EntityType);
        ArgumentException.ThrowIfNullOrWhiteSpace(command.EntityId);
        EntityType = command.EntityType;
        EntityId = command.EntityId;
        Action = command.Action;
        OldValue = command.OldValue;
        NewValue = command.NewValue;
        ChangedBy = command.ChangedBy;
        Reason = command.Reason;
        ChangedAt = DateTime.UtcNow;
    }

    public AuditTrailEntryId Id { get; }

    /// <summary>
    ///     Gets the type of the changed record, for example <c>QualityDocument</c>.
    /// </summary>
    public string EntityType { get; private set; }

    public string EntityId { get; private set; }
    public EAuditAction Action { get; private set; }
    public string? OldValue { get; private set; }
    public string? NewValue { get; private set; }
    public UserId ChangedBy { get; private set; }
    public DateTime ChangedAt { get; private set; }
    public string? Reason { get; private set; }
}
