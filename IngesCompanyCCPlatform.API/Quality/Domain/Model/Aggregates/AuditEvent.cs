using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Audit event of the Quality &amp; Compliance bounded context, as in <c>class-06-quality</c>.
/// </summary>
public partial class AuditEvent : AggregateRoot
{
    private const string EventCodeNotBlankMessageKey = "quality.auditEvent.error.eventCode.notBlank";
    private const string EventNotBlankMessageKey = "quality.auditEvent.error.event.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected AuditEvent()
    {
        EventCode = string.Empty;
        Actor = string.Empty;
        ActorRole = string.Empty;
        Event = string.Empty;
        Record = string.Empty;
        Result = string.Empty;
        Field = string.Empty;
        Before = string.Empty;
        After = string.Empty;
        Reason = string.Empty;
    }

    /// <summary>
    ///     Creates the audit event from its command.
    /// </summary>
    /// <param name="command">The data of the audit event.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public AuditEvent(CreateAuditEventCommand command) : this() => Apply(command.EventCode, command.OccurredAt, command.Actor, command.ActorRole, command.Event, command.Record, command.Result, command.Field, command.Before, command.After, command.Reason);

    /// <summary>
    ///     The id of the audit event.
    /// </summary>
    public AuditEventId Id { get; }

    /// <summary>
    ///     The event code.
    /// </summary>
    public string EventCode { get; private set; }

    /// <summary>
    ///     The occurred at.
    /// </summary>
    public DateTime? OccurredAt { get; private set; }

    /// <summary>
    ///     The actor.
    /// </summary>
    public string Actor { get; private set; }

    /// <summary>
    ///     The actor role.
    /// </summary>
    public string ActorRole { get; private set; }

    /// <summary>
    ///     The event.
    /// </summary>
    public string Event { get; private set; }

    /// <summary>
    ///     The record.
    /// </summary>
    public string Record { get; private set; }

    /// <summary>
    ///     The result.
    /// </summary>
    public string Result { get; private set; }

    /// <summary>
    ///     The field.
    /// </summary>
    public string Field { get; private set; }

    /// <summary>
    ///     The before.
    /// </summary>
    public string Before { get; private set; }

    /// <summary>
    ///     The after.
    /// </summary>
    public string After { get; private set; }

    /// <summary>
    ///     The reason.
    /// </summary>
    public string Reason { get; private set; }

    private void Apply(string eventCode, DateTime? occurredAt, string actor, string actorRole, string @event, string record, string result, string field, string before, string after, string reason)
    {
        if (string.IsNullOrWhiteSpace(eventCode)) throw new ArgumentException(EventCodeNotBlankMessageKey);
        EventCode = eventCode?.Trim() ?? string.Empty;
        OccurredAt = occurredAt ?? DateTime.UtcNow;
        Actor = actor?.Trim() ?? string.Empty;
        ActorRole = actorRole?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(@event)) throw new ArgumentException(EventNotBlankMessageKey);
        Event = @event?.Trim() ?? string.Empty;
        Record = record?.Trim() ?? string.Empty;
        Result = result?.Trim() ?? string.Empty;
        Field = field?.Trim() ?? string.Empty;
        Before = before?.Trim() ?? string.Empty;
        After = after?.Trim() ?? string.Empty;
        Reason = reason?.Trim() ?? string.Empty;
    }
}
