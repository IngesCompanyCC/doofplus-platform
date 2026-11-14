using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     An entry of the audit trail: what happened to a record, who did it and when.
/// </summary>
public partial class AuditEvent : AggregateRoot
{
    private const string AggregateTypeNotBlankMessageKey = "quality.auditEvent.error.aggregateType.notBlank";
    private const string EventTypeNotBlankMessageKey = "quality.auditEvent.error.eventType.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected AuditEvent()
    {
        AggregateType = string.Empty;
        EventType = string.Empty;
        Description = string.Empty;
        PerformedBy = string.Empty;
    }

    /// <summary>
    ///     Creates the audit event from its command.
    /// </summary>
    /// <param name="command">The data of the audit event.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public AuditEvent(CreateAuditEventCommand command) : this()
    {
        if (string.IsNullOrWhiteSpace(command.AggregateType)) throw new ArgumentException(AggregateTypeNotBlankMessageKey);
        AggregateType = command.AggregateType.Trim();
        AggregateId = command.AggregateId;
        if (string.IsNullOrWhiteSpace(command.EventType)) throw new ArgumentException(EventTypeNotBlankMessageKey);
        EventType = command.EventType.Trim();
        Description = command.Description?.Trim() ?? string.Empty;
        PerformedBy = command.PerformedBy?.Trim() ?? string.Empty;
        OccurredAt = DateTime.UtcNow;
    }

    /// <summary>
    ///     The id of the audit event.
    /// </summary>
    public AuditEventId Id { get; }

    /// <summary>
    ///     The kind of record, for example BatchRelease.
    /// </summary>
    public string AggregateType { get; private set; }
    /// <summary>
    ///     The id of the record.
    /// </summary>
    public int AggregateId { get; private set; }
    /// <summary>
    ///     What happened, for example Approved.
    /// </summary>
    public string EventType { get; private set; }
    /// <summary>
    ///     The detail of the event.
    /// </summary>
    public string Description { get; private set; }
    /// <summary>
    ///     Who did it.
    /// </summary>
    public string PerformedBy { get; private set; }
    /// <summary>
    ///     When it happened (UTC).
    /// </summary>
    public DateTime OccurredAt { get; private set; }
}
