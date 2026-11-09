using System.Globalization;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Entities;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Represents the QualityDocument aggregate root within the Quality &amp; Compliance bounded context.
/// </summary>
/// <remarks>
///     A controlled document (SOP, protocol or specification) keeps all its revisions. A revision goes from
///     draft to review and is approved with an electronic signature by someone who did not write it.
/// </remarks>
public partial class QualityDocument : AggregateRoot
{
    private const string InitialVersionNumber = "1.0";
    private const string InitialChangeReason = "Initial version";

    private readonly List<DocumentVersion> _versions = [];

    /// <summary>
    ///     Initializes a new instance of the <see cref="QualityDocument" /> aggregate. Used by Entity Framework Core.
    /// </summary>
    protected QualityDocument()
    {
        Code = string.Empty;
        Title = string.Empty;
    }

    /// <summary>
    ///     Initializes a new quality document in draft with its first version.
    /// </summary>
    /// <param name="command">The <see cref="CreateQualityDocumentCommand" /> with the document data.</param>
    public QualityDocument(CreateQualityDocumentCommand command) : this()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(command.Code);
        ArgumentException.ThrowIfNullOrWhiteSpace(command.Title);
        Code = command.Code;
        Title = command.Title;
        Type = command.Type;
        Status = EDocumentStatus.Draft;
        _versions.Add(new DocumentVersion(InitialVersionNumber, command.FileUrl, InitialChangeReason, command.AuthorId));
    }

    public QualityDocumentId Id { get; }

    /// <summary>
    ///     Gets the unique code of the document, for example <c>SOP-QA-014</c>.
    /// </summary>
    public string Code { get; private set; }

    public string Title { get; private set; }
    public EDocumentType Type { get; private set; }
    public EDocumentStatus Status { get; private set; }

    /// <summary>
    ///     Gets the revisions of the document, oldest first.
    /// </summary>
    public IReadOnlyCollection<DocumentVersion> Versions => _versions.AsReadOnly();

    /// <summary>
    ///     Gets the latest revision of the document.
    /// </summary>
    public DocumentVersion CurrentVersion =>
        _versions.MaxBy(version => decimal.Parse(version.VersionNumber, CultureInfo.InvariantCulture))!;

    /// <summary>
    ///     Sends the current revision to review.
    /// </summary>
    public void SubmitForReview()
    {
        if (Status != EDocumentStatus.Draft)
            throw new InvalidOperationException("Only a draft document can be submitted for review.");
        Status = EDocumentStatus.InReview;
    }

    /// <summary>
    ///     Approves the current revision. The author of the revision cannot approve it (self-approval).
    /// </summary>
    /// <param name="approverId">The user that approves the revision.</param>
    /// <param name="signatureId">The electronic signature registered in IAM.</param>
    public void Approve(UserId approverId, ElectronicSignatureId signatureId)
    {
        if (Status != EDocumentStatus.InReview)
            throw new InvalidOperationException("Only a document in review can be approved.");
        if (CurrentVersion.AuthorId == approverId)
            throw new InvalidOperationException("The author of a revision cannot approve it.");
        CurrentVersion.Approve(signatureId);
        Status = EDocumentStatus.Approved;
    }

    /// <summary>
    ///     Sends a document in review back to draft.
    /// </summary>
    /// <param name="reason">Why the revision was rejected.</param>
    public void Reject(string reason)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reason);
        if (Status != EDocumentStatus.InReview)
            throw new InvalidOperationException("Only a document in review can be rejected.");
        Status = EDocumentStatus.Draft;
    }

    /// <summary>
    ///     Adds a new revision to an approved document. The document goes back to draft.
    /// </summary>
    /// <param name="fileUrl">The URL of the file of the new revision.</param>
    /// <param name="changeReason">Why the revision was written.</param>
    /// <param name="authorId">The user that wrote the revision.</param>
    /// <returns>The new <see cref="DocumentVersion" />.</returns>
    public DocumentVersion PublishNewVersion(string fileUrl, string changeReason, UserId authorId)
    {
        if (Status != EDocumentStatus.Approved)
            throw new InvalidOperationException("A new version can only be published from an approved document.");
        var version = new DocumentVersion(NextVersionNumber(), fileUrl, changeReason, authorId);
        _versions.Add(version);
        Status = EDocumentStatus.Draft;
        return version;
    }

    private string NextVersionNumber()
    {
        var current = decimal.Parse(CurrentVersion.VersionNumber, CultureInfo.InvariantCulture);
        return (current + 0.1m).ToString("0.0", CultureInfo.InvariantCulture);
    }
}
