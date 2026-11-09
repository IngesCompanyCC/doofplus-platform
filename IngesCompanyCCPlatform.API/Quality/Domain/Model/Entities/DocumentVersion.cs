using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Entities;

/// <summary>
///     Represents one revision of a quality document, for example version <c>2.4</c> of an SOP.
/// </summary>
public class DocumentVersion
{
    private const string FileUrlNotBlankMessageKey = "qualityDocument.error.fileUrl.notBlank";
    private const string ChangeReasonNotBlankMessageKey = "qualityDocument.error.changeReason.notBlank";

    /// <summary>
    ///     Initializes a new instance of the <see cref="DocumentVersion" /> entity. Used by Entity Framework Core.
    /// </summary>
    protected DocumentVersion()
    {
        VersionNumber = string.Empty;
        FileUrl = string.Empty;
        ChangeReason = string.Empty;
    }

    /// <summary>
    ///     Initializes a new revision.
    /// </summary>
    /// <param name="versionNumber">The version number, for example <c>1.0</c>.</param>
    /// <param name="fileUrl">The URL of the file of the revision.</param>
    /// <param name="changeReason">Why the revision was written.</param>
    /// <param name="authorId">The user that wrote the revision.</param>
    public DocumentVersion(string versionNumber, string fileUrl, string changeReason, UserId authorId)
    {
        if (string.IsNullOrWhiteSpace(fileUrl)) throw new ArgumentException(FileUrlNotBlankMessageKey);
        if (string.IsNullOrWhiteSpace(changeReason)) throw new ArgumentException(ChangeReasonNotBlankMessageKey);
        VersionNumber = versionNumber;
        FileUrl = fileUrl;
        ChangeReason = changeReason;
        AuthorId = authorId;
    }

    public int Id { get; private set; }
    public string VersionNumber { get; private set; }
    public string FileUrl { get; private set; }
    public string ChangeReason { get; private set; }
    public UserId AuthorId { get; private set; }

    /// <summary>
    ///     Gets the electronic signature that approved the revision, or <c>null</c> while it is not approved.
    /// </summary>
    public ElectronicSignatureId? ApprovalSignatureId { get; private set; }

    /// <summary>
    ///     Records the electronic signature that approves the revision.
    /// </summary>
    /// <param name="signatureId">The electronic signature registered in IAM.</param>
    public void Approve(ElectronicSignatureId signatureId)
    {
        ApprovalSignatureId = signatureId;
    }
}
