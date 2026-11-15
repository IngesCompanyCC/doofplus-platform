using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Deviation of the Quality &amp; Compliance bounded context, as in <c>class-06-quality</c>.
/// </summary>
public partial class Deviation : AggregateRoot
{
    private const string CodeNotBlankMessageKey = "quality.deviation.error.code.notBlank";
    private const string TitleNotBlankMessageKey = "quality.deviation.error.title.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected Deviation()
    {
        Code = string.Empty;
        Title = string.Empty;
        BatchCode = string.Empty;
        OrderCode = string.Empty;
        Summary = string.Empty;
        Containment = string.Empty;
        RootCause = string.Empty;
        CapaCode = string.Empty;
        ReportedBy = string.Empty;
        Owner = string.Empty;
    }

    /// <summary>
    ///     Creates the deviation from its command.
    /// </summary>
    /// <param name="command">The data of the deviation.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public Deviation(CreateDeviationCommand command) : this() => Apply(command.Code, command.Title, command.BatchCode, command.OrderCode, command.Summary, command.Classification, command.Containment, command.Severity, command.Likelihood, command.Detectability, command.Status, command.RootCause, command.CapaCode, command.ReportedBy, command.ReportedAt, command.Owner, command.DueAt);

    /// <summary>
    ///     The id of the deviation.
    /// </summary>
    public DeviationId Id { get; }

    /// <summary>
    ///     The code.
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    ///     The title.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    ///     The batch code.
    /// </summary>
    public string BatchCode { get; private set; }

    /// <summary>
    ///     The order code.
    /// </summary>
    public string OrderCode { get; private set; }

    /// <summary>
    ///     The summary.
    /// </summary>
    public string Summary { get; private set; }

    /// <summary>
    ///     The classification.
    /// </summary>
    public EClassification Classification { get; private set; }

    /// <summary>
    ///     The containment.
    /// </summary>
    public string Containment { get; private set; }

    /// <summary>
    ///     The severity.
    /// </summary>
    public int Severity { get; private set; }

    /// <summary>
    ///     The likelihood.
    /// </summary>
    public int Likelihood { get; private set; }

    /// <summary>
    ///     The detectability.
    /// </summary>
    public int Detectability { get; private set; }

    /// <summary>
    ///     The status.
    /// </summary>
    public EDeviationStatus Status { get; private set; }

    /// <summary>
    ///     The root cause.
    /// </summary>
    public string RootCause { get; private set; }

    /// <summary>
    ///     The capa code.
    /// </summary>
    public string CapaCode { get; private set; }

    /// <summary>
    ///     The reported by.
    /// </summary>
    public string ReportedBy { get; private set; }

    /// <summary>
    ///     The reported at.
    /// </summary>
    public DateTime? ReportedAt { get; private set; }

    /// <summary>
    ///     The owner.
    /// </summary>
    public string Owner { get; private set; }

    /// <summary>
    ///     The due at.
    /// </summary>
    public DateTime? DueAt { get; private set; }

    /// <summary>
    ///     Replaces the data of the deviation with the data of the command.
    /// </summary>
    /// <param name="command">The new data.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public void Update(UpdateDeviationCommand command) => Apply(command.Code, command.Title, command.BatchCode, command.OrderCode, command.Summary, command.Classification, command.Containment, command.Severity, command.Likelihood, command.Detectability, command.Status, command.RootCause, command.CapaCode, command.ReportedBy, command.ReportedAt, command.Owner, command.DueAt);

    private void Apply(string code, string title, string batchCode, string orderCode, string summary, EClassification classification, string containment, int severity, int likelihood, int detectability, EDeviationStatus status, string rootCause, string capaCode, string reportedBy, DateTime? reportedAt, string owner, DateTime? dueAt)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException(CodeNotBlankMessageKey);
        Code = code?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException(TitleNotBlankMessageKey);
        Title = title?.Trim() ?? string.Empty;
        BatchCode = batchCode?.Trim() ?? string.Empty;
        OrderCode = orderCode?.Trim() ?? string.Empty;
        Summary = summary?.Trim() ?? string.Empty;
        Classification = classification;
        Containment = containment?.Trim() ?? string.Empty;
        Severity = severity;
        Likelihood = likelihood;
        Detectability = detectability;
        Status = status;
        RootCause = rootCause?.Trim() ?? string.Empty;
        CapaCode = capaCode?.Trim() ?? string.Empty;
        ReportedBy = reportedBy?.Trim() ?? string.Empty;
        ReportedAt = reportedAt;
        Owner = owner?.Trim() ?? string.Empty;
        DueAt = dueAt;
    }
}
