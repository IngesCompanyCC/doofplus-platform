using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     An internal or external audit of the quality system.
/// </summary>
public partial class Audit : AggregateRoot
{
    private const string CodeNotBlankMessageKey = "quality.audit.error.code.notBlank";
    private const string TitleNotBlankMessageKey = "quality.audit.error.title.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected Audit()
    {
        Code = string.Empty;
        Title = string.Empty;
        Scope = string.Empty;
        AuditType = string.Empty;
    }

    /// <summary>
    ///     Creates the audit from its command.
    /// </summary>
    /// <param name="command">The data of the audit.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public Audit(CreateAuditCommand command) : this()
    {
        if (string.IsNullOrWhiteSpace(command.Code)) throw new ArgumentException(CodeNotBlankMessageKey);
        Code = command.Code.Trim();
        if (string.IsNullOrWhiteSpace(command.Title)) throw new ArgumentException(TitleNotBlankMessageKey);
        Title = command.Title.Trim();
        Scope = command.Scope?.Trim() ?? string.Empty;
        AuditType = command.AuditType?.Trim() ?? string.Empty;
        AuditorId = command.AuditorId;
        PlannedDate = command.PlannedDate;
        Status = EAuditStatus.Planned;
    }

    /// <summary>
    ///     The id of the audit.
    /// </summary>
    public AuditId Id { get; }

    /// <summary>
    ///     The business code, for example AUD-2026-03.
    /// </summary>
    public string Code { get; private set; }
    /// <summary>
    ///     The title of the audit.
    /// </summary>
    public string Title { get; private set; }
    /// <summary>
    ///     What the audit covers.
    /// </summary>
    public string Scope { get; private set; }
    /// <summary>
    ///     The kind of audit, for example internal.
    /// </summary>
    public string AuditType { get; private set; }
    /// <summary>
    ///     The id of the auditor.
    /// </summary>
    public int? AuditorId { get; private set; }
    /// <summary>
    ///     When the audit is planned.
    /// </summary>
    public DateOnly? PlannedDate { get; private set; }
    /// <summary>
    ///     Where the audit is.
    /// </summary>
    public EAuditStatus Status { get; private set; }
    /// <summary>
    ///     When the audit started (UTC).
    /// </summary>
    public DateTime? StartedAt { get; private set; }
    /// <summary>
    ///     When the audit was completed (UTC).
    /// </summary>
    public DateTime? CompletedAt { get; private set; }

    /// <summary>
    ///     Starts a planned audit.
    /// </summary>
    public bool Start()
    {
        if (Status != EAuditStatus.Planned) return false;
        Status = EAuditStatus.InProgress;
        StartedAt = DateTime.UtcNow;
        return true;
    }

    /// <summary>
    ///     Completes an audit in progress.
    /// </summary>
    public bool Complete()
    {
        if (Status != EAuditStatus.InProgress) return false;
        Status = EAuditStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        return true;
    }

    /// <summary>
    ///     Cancels an audit that is not completed.
    /// </summary>
    public bool Cancel()
    {
        if (Status == EAuditStatus.Completed) return false;
        Status = EAuditStatus.Cancelled;
        return true;
    }
}
