using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     A controlled document of the quality system, for example an SOP.
/// </summary>
public partial class QualityDocument : AggregateRoot
{
    private const string CodeNotBlankMessageKey = "quality.qualityDocument.error.code.notBlank";
    private const string TitleNotBlankMessageKey = "quality.qualityDocument.error.title.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected QualityDocument()
    {
        Code = string.Empty;
        Title = string.Empty;
        Description = string.Empty;
        DocumentType = string.Empty;
        FileReference = string.Empty;
        Version = string.Empty;
    }

    /// <summary>
    ///     Creates the quality document from its command.
    /// </summary>
    /// <param name="command">The data of the quality document.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public QualityDocument(CreateQualityDocumentCommand command) : this()
    {
        if (string.IsNullOrWhiteSpace(command.Code)) throw new ArgumentException(CodeNotBlankMessageKey);
        Code = command.Code.Trim();
        if (string.IsNullOrWhiteSpace(command.Title)) throw new ArgumentException(TitleNotBlankMessageKey);
        Title = command.Title.Trim();
        Description = command.Description?.Trim() ?? string.Empty;
        DocumentType = command.DocumentType?.Trim() ?? string.Empty;
        FileReference = command.FileReference?.Trim() ?? string.Empty;
        Version = command.Version?.Trim() ?? string.Empty;
        OwnerId = command.OwnerId;
        Status = EQualityDocumentStatus.Draft;
    }

    /// <summary>
    ///     The id of the quality document.
    /// </summary>
    public QualityDocumentId Id { get; }

    /// <summary>
    ///     The business code, for example SOP-QC-014.
    /// </summary>
    public string Code { get; private set; }
    /// <summary>
    ///     The title of the document.
    /// </summary>
    public string Title { get; private set; }
    /// <summary>
    ///     What the document covers.
    /// </summary>
    public string Description { get; private set; }
    /// <summary>
    ///     The kind of document, for example SOP or specification.
    /// </summary>
    public string DocumentType { get; private set; }
    /// <summary>
    ///     Where the file of the document is.
    /// </summary>
    public string FileReference { get; private set; }
    /// <summary>
    ///     The version of the document.
    /// </summary>
    public string Version { get; private set; }
    /// <summary>
    ///     The id of the user who owns the document.
    /// </summary>
    public int? OwnerId { get; private set; }
    /// <summary>
    ///     Where the document is.
    /// </summary>
    public EQualityDocumentStatus Status { get; private set; }
    /// <summary>
    ///     When the document was approved (UTC).
    /// </summary>
    public DateTime? ApprovedAt { get; private set; }

    /// <summary>
    ///     Sends a draft to review.
    /// </summary>
    public bool SubmitForReview()
    {
        if (Status != EQualityDocumentStatus.Draft) return false;
        Status = EQualityDocumentStatus.UnderReview;
        return true;
    }

    /// <summary>
    ///     Approves a document under review.
    /// </summary>
    public bool Approve()
    {
        if (Status != EQualityDocumentStatus.UnderReview) return false;
        Status = EQualityDocumentStatus.Approved;
        ApprovedAt = DateTime.UtcNow;
        return true;
    }

    /// <summary>
    ///     Marks an approved document as obsolete.
    /// </summary>
    public bool MarkObsolete()
    {
        if (Status != EQualityDocumentStatus.Approved) return false;
        Status = EQualityDocumentStatus.Obsolete;
        return true;
    }
}
