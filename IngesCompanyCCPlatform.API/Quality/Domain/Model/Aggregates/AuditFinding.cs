using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     A finding of an audit.
/// </summary>
public partial class AuditFinding : AggregateRoot
{
    private const string TitleNotBlankMessageKey = "quality.auditFinding.error.title.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected AuditFinding()
    {
        Title = string.Empty;
        Description = string.Empty;
    }

    /// <summary>
    ///     Creates the audit finding from its command.
    /// </summary>
    /// <param name="command">The data of the audit finding.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public AuditFinding(CreateAuditFindingCommand command) : this()
    {
        AuditId = command.AuditId;
        if (string.IsNullOrWhiteSpace(command.Title)) throw new ArgumentException(TitleNotBlankMessageKey);
        Title = command.Title.Trim();
        Description = command.Description?.Trim() ?? string.Empty;
        Severity = command.Severity;
        RelatedDeviationId = command.RelatedDeviationId;
        IdentifiedAt = DateTime.UtcNow;
    }

    /// <summary>
    ///     The id of the audit finding.
    /// </summary>
    public AuditFindingId Id { get; }

    /// <summary>
    ///     The id of the audit.
    /// </summary>
    public int AuditId { get; private set; }
    /// <summary>
    ///     The title of the finding.
    /// </summary>
    public string Title { get; private set; }
    /// <summary>
    ///     What was found.
    /// </summary>
    public string Description { get; private set; }
    /// <summary>
    ///     How serious the finding is.
    /// </summary>
    public EAuditFindingSeverity Severity { get; private set; }
    /// <summary>
    ///     The id of the related deviation.
    /// </summary>
    public int? RelatedDeviationId { get; private set; }
    /// <summary>
    ///     When the finding was registered (UTC).
    /// </summary>
    public DateTime IdentifiedAt { get; private set; }
}
