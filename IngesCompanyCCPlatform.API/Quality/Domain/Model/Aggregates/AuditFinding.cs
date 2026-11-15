using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Audit finding of the Quality &amp; Compliance bounded context, as in <c>class-06-quality</c>.
/// </summary>
public partial class AuditFinding : AggregateRoot
{
    private const string AuditCodeNotBlankMessageKey = "quality.auditFinding.error.auditCode.notBlank";
    private const string CodeNotBlankMessageKey = "quality.auditFinding.error.code.notBlank";
    private const string TitleNotBlankMessageKey = "quality.auditFinding.error.title.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected AuditFinding()
    {
        AuditCode = string.Empty;
        Code = string.Empty;
        Title = string.Empty;
        Owner = string.Empty;
        Observation = string.Empty;
        Response = string.Empty;
        ClosureRequirement = string.Empty;
    }

    /// <summary>
    ///     Creates the audit finding from its command.
    /// </summary>
    /// <param name="command">The data of the audit finding.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public AuditFinding(CreateAuditFindingCommand command) : this() => Apply(command.AuditCode, command.Code, command.Title, command.Classification, command.Owner, command.DueAt, command.Status, command.Observation, command.Response, command.ClosureRequirement);

    /// <summary>
    ///     The id of the audit finding.
    /// </summary>
    public AuditFindingId Id { get; }

    /// <summary>
    ///     The audit code.
    /// </summary>
    public string AuditCode { get; private set; }

    /// <summary>
    ///     The code.
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    ///     The title.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    ///     The classification.
    /// </summary>
    public EClassification Classification { get; private set; }

    /// <summary>
    ///     The owner.
    /// </summary>
    public string Owner { get; private set; }

    /// <summary>
    ///     The due at.
    /// </summary>
    public DateOnly? DueAt { get; private set; }

    /// <summary>
    ///     The status.
    /// </summary>
    public EFindingStatus Status { get; private set; }

    /// <summary>
    ///     The observation.
    /// </summary>
    public string Observation { get; private set; }

    /// <summary>
    ///     The response.
    /// </summary>
    public string Response { get; private set; }

    /// <summary>
    ///     The closure requirement.
    /// </summary>
    public string ClosureRequirement { get; private set; }

    private void Apply(string auditCode, string code, string title, EClassification classification, string owner, DateOnly? dueAt, EFindingStatus status, string observation, string response, string closureRequirement)
    {
        if (string.IsNullOrWhiteSpace(auditCode)) throw new ArgumentException(AuditCodeNotBlankMessageKey);
        AuditCode = auditCode?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException(CodeNotBlankMessageKey);
        Code = code?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException(TitleNotBlankMessageKey);
        Title = title?.Trim() ?? string.Empty;
        Classification = classification;
        Owner = owner?.Trim() ?? string.Empty;
        DueAt = dueAt;
        Status = status;
        Observation = observation?.Trim() ?? string.Empty;
        Response = response?.Trim() ?? string.Empty;
        ClosureRequirement = closureRequirement?.Trim() ?? string.Empty;
    }
}
