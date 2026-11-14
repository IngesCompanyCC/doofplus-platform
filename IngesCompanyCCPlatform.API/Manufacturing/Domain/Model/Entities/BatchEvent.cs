using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Entities;

/// <summary>
///     Represents an audit event in the lifecycle of a production batch.
/// </summary>
public class BatchEvent
{
    public int Id { get; private set; }
    public int ProductionBatchId { get; private set; }
    public string Type { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public EventTone Tone { get; private set; }
    public long? PerformedBy { get; private set; }
    public DateTime OccurredAt { get; private set; }

    protected BatchEvent()
    {
    }

    public BatchEvent(string type, string description, EventTone tone = EventTone.Normal, long? performedBy = null)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Event type cannot be empty.", nameof(type));

        Type = type.Trim();
        Description = description?.Trim() ?? string.Empty;
        Tone = tone;
        PerformedBy = performedBy;
        OccurredAt = DateTime.UtcNow;
    }
}
