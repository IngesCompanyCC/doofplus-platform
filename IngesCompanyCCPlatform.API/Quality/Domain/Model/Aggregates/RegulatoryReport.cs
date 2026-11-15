using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Regulatory report of the Quality &amp; Compliance bounded context, as in <c>class-06-quality</c>.
/// </summary>
public partial class RegulatoryReport : AggregateRoot
{
    private const string CodeNotBlankMessageKey = "quality.regulatoryReport.error.code.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected RegulatoryReport()
    {
        Code = string.Empty;
        Scope = string.Empty;
        Owner = string.Empty;
        Template = string.Empty;
        Format = string.Empty;
    }

    /// <summary>
    ///     Creates the regulatory report from its command.
    /// </summary>
    /// <param name="command">The data of the regulatory report.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public RegulatoryReport(CreateRegulatoryReportCommand command) : this() => Apply(command.Code, command.Type, command.Scope, command.Owner, command.Status, command.Template, command.Format, command.CreatedOn);

    /// <summary>
    ///     The id of the regulatory report.
    /// </summary>
    public RegulatoryReportId Id { get; }

    /// <summary>
    ///     The code.
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    ///     The type.
    /// </summary>
    public EReportType Type { get; private set; }

    /// <summary>
    ///     The scope.
    /// </summary>
    public string Scope { get; private set; }

    /// <summary>
    ///     The owner.
    /// </summary>
    public string Owner { get; private set; }

    /// <summary>
    ///     The status.
    /// </summary>
    public EReportStatus Status { get; private set; }

    /// <summary>
    ///     The template.
    /// </summary>
    public string Template { get; private set; }

    /// <summary>
    ///     The format.
    /// </summary>
    public string Format { get; private set; }

    /// <summary>
    ///     The created at.
    /// </summary>
    public DateOnly? CreatedOn { get; private set; }

    private void Apply(string code, EReportType type, string scope, string owner, EReportStatus status, string template, string format, DateOnly? createdAt)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException(CodeNotBlankMessageKey);
        Code = code?.Trim() ?? string.Empty;
        Type = type;
        Scope = scope?.Trim() ?? string.Empty;
        Owner = owner?.Trim() ?? string.Empty;
        Status = status;
        Template = template?.Trim() ?? string.Empty;
        Format = format?.Trim() ?? string.Empty;
        CreatedOn = createdAt ?? DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
