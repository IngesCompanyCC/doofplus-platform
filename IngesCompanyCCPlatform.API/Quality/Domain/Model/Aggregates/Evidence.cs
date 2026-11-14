using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     A piece of evidence attached to a deviation or a document.
/// </summary>
public partial class Evidence : AggregateRoot
{
    private const string TitleNotBlankMessageKey = "quality.evidence.error.title.notBlank";
    private const string FileReferenceNotBlankMessageKey = "quality.evidence.error.fileReference.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected Evidence()
    {
        Title = string.Empty;
        Description = string.Empty;
        FileReference = string.Empty;
    }

    /// <summary>
    ///     Creates the evidence from its command.
    /// </summary>
    /// <param name="command">The data of the evidence.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public Evidence(CreateEvidenceCommand command) : this()
    {
        if (string.IsNullOrWhiteSpace(command.Title)) throw new ArgumentException(TitleNotBlankMessageKey);
        Title = command.Title.Trim();
        Description = command.Description?.Trim() ?? string.Empty;
        Type = command.Type;
        if (string.IsNullOrWhiteSpace(command.FileReference)) throw new ArgumentException(FileReferenceNotBlankMessageKey);
        FileReference = command.FileReference.Trim();
        DeviationId = command.DeviationId;
        DocumentId = command.DocumentId;
        UploadedBy = command.UploadedBy;
        Status = EEvidenceStatus.Pending;
        UploadedAt = DateTime.UtcNow;
    }

    /// <summary>
    ///     The id of the evidence.
    /// </summary>
    public EvidenceId Id { get; }

    /// <summary>
    ///     The title of the evidence.
    /// </summary>
    public string Title { get; private set; }
    /// <summary>
    ///     What the evidence shows.
    /// </summary>
    public string Description { get; private set; }
    /// <summary>
    ///     The kind of evidence.
    /// </summary>
    public EEvidenceType Type { get; private set; }
    /// <summary>
    ///     Where the file of the evidence is.
    /// </summary>
    public string FileReference { get; private set; }
    /// <summary>
    ///     The id of the related deviation.
    /// </summary>
    public int? DeviationId { get; private set; }
    /// <summary>
    ///     The id of the related quality document.
    /// </summary>
    public int? DocumentId { get; private set; }
    /// <summary>
    ///     The id of the user who uploaded it.
    /// </summary>
    public int? UploadedBy { get; private set; }
    /// <summary>
    ///     Where the evidence is in its verification.
    /// </summary>
    public EEvidenceStatus Status { get; private set; }
    /// <summary>
    ///     When the evidence was uploaded (UTC).
    /// </summary>
    public DateTime UploadedAt { get; private set; }
    /// <summary>
    ///     When the evidence was verified (UTC).
    /// </summary>
    public DateTime? VerifiedAt { get; private set; }

    /// <summary>
    ///     Verifies pending evidence.
    /// </summary>
    public bool Verify()
    {
        if (Status != EEvidenceStatus.Pending) return false;
        Status = EEvidenceStatus.Verified;
        VerifiedAt = DateTime.UtcNow;
        return true;
    }

    /// <summary>
    ///     Rejects pending evidence.
    /// </summary>
    public bool Reject()
    {
        if (Status != EEvidenceStatus.Pending) return false;
        Status = EEvidenceStatus.Rejected;
        return true;
    }
}
