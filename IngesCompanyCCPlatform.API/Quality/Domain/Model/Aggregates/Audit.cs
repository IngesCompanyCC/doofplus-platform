using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Audit of the Quality &amp; Compliance bounded context, as in <c>class-06-quality</c>.
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
        Period = string.Empty;
    }

    /// <summary>
    ///     Creates the audit from its command.
    /// </summary>
    /// <param name="command">The data of the audit.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public Audit(CreateAuditCommand command) : this() => Apply(command.Code, command.Title, command.Period, command.AreasReviewed, command.AreasTotal, command.Status);

    /// <summary>
    ///     The id of the audit.
    /// </summary>
    public AuditId Id { get; }

    /// <summary>
    ///     The code.
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    ///     The title.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    ///     The period.
    /// </summary>
    public string Period { get; private set; }

    /// <summary>
    ///     The areas reviewed.
    /// </summary>
    public int AreasReviewed { get; private set; }

    /// <summary>
    ///     The areas total.
    /// </summary>
    public int AreasTotal { get; private set; }

    /// <summary>
    ///     The status.
    /// </summary>
    public EAuditStatus Status { get; private set; }

    private void Apply(string code, string title, string period, int areasReviewed, int areasTotal, EAuditStatus status)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException(CodeNotBlankMessageKey);
        Code = code?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException(TitleNotBlankMessageKey);
        Title = title?.Trim() ?? string.Empty;
        Period = period?.Trim() ?? string.Empty;
        AreasReviewed = areasReviewed;
        AreasTotal = areasTotal;
        Status = status;
    }
}
