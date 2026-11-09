using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Entities;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Represents the Audit aggregate root within the Quality &amp; Compliance bounded context.
/// </summary>
/// <remarks>
///     An internal or regulatory audit is scheduled, started, records its findings and observations,
///     receives the evidence package and is closed.
/// </remarks>
public partial class Audit : AggregateRoot
{
    private readonly List<AuditFinding> _findings = [];

    /// <summary>
    ///     Initializes a new instance of the <see cref="Audit" /> aggregate. Used by Entity Framework Core.
    /// </summary>
    protected Audit()
    {
    }

    /// <summary>
    ///     Initializes a scheduled audit.
    /// </summary>
    /// <param name="command">The <see cref="ScheduleAuditCommand" /> with the audit data.</param>
    public Audit(ScheduleAuditCommand command) : this()
    {
        Type = command.Type;
        ScheduledDate = command.ScheduledDate;
        Status = EAuditStatus.Scheduled;
    }

    public AuditId Id { get; }
    public EAuditType Type { get; private set; }
    public DateOnly ScheduledDate { get; private set; }
    public EAuditStatus Status { get; private set; }
    public string? EvidencePackageUrl { get; private set; }
    public IReadOnlyCollection<AuditFinding> Findings => _findings.AsReadOnly();

    /// <summary>
    ///     Starts a scheduled audit.
    /// </summary>
    public void Start()
    {
        if (Status != EAuditStatus.Scheduled)
            throw new InvalidOperationException("Only a scheduled audit can be started.");
        Status = EAuditStatus.InProgress;
    }

    /// <summary>
    ///     Records a finding or an observation.
    /// </summary>
    /// <param name="type">Whether it is a finding or an observation.</param>
    /// <param name="description">What the auditor found.</param>
    /// <returns>The new <see cref="AuditFinding" />.</returns>
    public AuditFinding RegisterFinding(EFindingType type, string description)
    {
        EnsureInProgress();
        var finding = new AuditFinding(type, description);
        _findings.Add(finding);
        return finding;
    }

    /// <summary>
    ///     Attaches the evidence package delivered to the auditor.
    /// </summary>
    /// <param name="fileUrl">The URL of the evidence package.</param>
    public void AttachEvidencePackage(string fileUrl)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fileUrl);
        EnsureInProgress();
        EvidencePackageUrl = fileUrl;
    }

    /// <summary>
    ///     Closes the audit.
    /// </summary>
    public void Close()
    {
        EnsureInProgress();
        Status = EAuditStatus.Closed;
    }

    private void EnsureInProgress()
    {
        if (Status != EAuditStatus.InProgress)
            throw new InvalidOperationException("The audit must be in progress.");
    }
}
