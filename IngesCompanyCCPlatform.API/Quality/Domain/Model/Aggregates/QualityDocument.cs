using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Quality document of the Quality &amp; Compliance bounded context, as in <c>class-06-quality</c>.
/// </summary>
public partial class QualityDocument : AggregateRoot
{
    private const string CodeNotBlankMessageKey = "quality.qualityDocument.error.code.notBlank";
    private const string VersionNotBlankMessageKey = "quality.qualityDocument.error.version.notBlank";
    private const string TitleNotBlankMessageKey = "quality.qualityDocument.error.title.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected QualityDocument()
    {
        Code = string.Empty;
        Version = string.Empty;
        Title = string.Empty;
        Author = string.Empty;
        TechnicalReviewer = string.Empty;
        Approver = string.Empty;
        Purpose = string.Empty;
        Scope = string.Empty;
        Procedure = string.Empty;
        RevisionSummary = string.Empty;
    }

    /// <summary>
    ///     Creates the quality document from its command.
    /// </summary>
    /// <param name="command">The data of the quality document.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public QualityDocument(CreateQualityDocumentCommand command) : this() => Apply(command.Code, command.Version, command.Title, command.Status, command.Author, command.TechnicalReviewer, command.Approver, command.ChangedAt, command.Purpose, command.Scope, command.Procedure, command.RevisionSummary, command.ReviewCycleMonths);

    /// <summary>
    ///     The id of the quality document.
    /// </summary>
    public QualityDocumentId Id { get; }

    /// <summary>
    ///     The code.
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    ///     The version.
    /// </summary>
    public string Version { get; private set; }

    /// <summary>
    ///     The title.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    ///     The status.
    /// </summary>
    public EDocumentStatus Status { get; private set; }

    /// <summary>
    ///     The author.
    /// </summary>
    public string Author { get; private set; }

    /// <summary>
    ///     The technical reviewer.
    /// </summary>
    public string TechnicalReviewer { get; private set; }

    /// <summary>
    ///     The approver.
    /// </summary>
    public string Approver { get; private set; }

    /// <summary>
    ///     The changed at.
    /// </summary>
    public DateOnly? ChangedAt { get; private set; }

    /// <summary>
    ///     The purpose.
    /// </summary>
    public string Purpose { get; private set; }

    /// <summary>
    ///     The scope.
    /// </summary>
    public string Scope { get; private set; }

    /// <summary>
    ///     The procedure.
    /// </summary>
    public string Procedure { get; private set; }

    /// <summary>
    ///     The revision summary.
    /// </summary>
    public string RevisionSummary { get; private set; }

    /// <summary>
    ///     The review cycle months.
    /// </summary>
    public int ReviewCycleMonths { get; private set; }

    /// <summary>
    ///     Replaces the data of the quality document with the data of the command.
    /// </summary>
    /// <param name="command">The new data.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public void Update(UpdateQualityDocumentCommand command) => Apply(command.Code, command.Version, command.Title, command.Status, command.Author, command.TechnicalReviewer, command.Approver, command.ChangedAt, command.Purpose, command.Scope, command.Procedure, command.RevisionSummary, command.ReviewCycleMonths);

    private void Apply(string code, string version, string title, EDocumentStatus status, string author, string technicalReviewer, string approver, DateOnly? changedAt, string purpose, string scope, string procedure, string revisionSummary, int reviewCycleMonths)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException(CodeNotBlankMessageKey);
        Code = code?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(version)) throw new ArgumentException(VersionNotBlankMessageKey);
        Version = version?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException(TitleNotBlankMessageKey);
        Title = title?.Trim() ?? string.Empty;
        Status = status;
        Author = author?.Trim() ?? string.Empty;
        TechnicalReviewer = technicalReviewer?.Trim() ?? string.Empty;
        Approver = approver?.Trim() ?? string.Empty;
        ChangedAt = changedAt;
        Purpose = purpose?.Trim() ?? string.Empty;
        Scope = scope?.Trim() ?? string.Empty;
        Procedure = procedure?.Trim() ?? string.Empty;
        RevisionSummary = revisionSummary?.Trim() ?? string.Empty;
        ReviewCycleMonths = reviewCycleMonths;
    }
}
