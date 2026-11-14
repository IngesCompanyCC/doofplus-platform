using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Entities;

/// <summary>
///     Represents an operational incident occurred during production batch execution.
/// </summary>
public class Incident
{
    public int Id { get; private set; }
    public int ProductionBatchId { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public IncidentSeverity Severity { get; private set; }
    public IncidentStatus Status { get; private set; }
    public DateTime RecordedAt { get; private set; }

    protected Incident()
    {
    }

    public Incident(string description, IncidentSeverity severity)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Incident description cannot be empty.", nameof(description));

        Description = description.Trim();
        Severity = severity;
        Status = IncidentStatus.Open;
        RecordedAt = DateTime.UtcNow;
    }

    public void Resolve()
    {
        Status = IncidentStatus.Resolved;
    }
}
