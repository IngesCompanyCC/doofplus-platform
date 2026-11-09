namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Entities;

/// <summary>
///     Represents a file attached as evidence to the review of a batch.
/// </summary>
public class EvidenceAttachment
{
    private const string FileUrlNotBlankMessageKey = "batchReview.error.evidence.fileUrl.notBlank";

    /// <summary>
    ///     Initializes a new instance of the <see cref="EvidenceAttachment" /> entity. Used by Entity Framework Core.
    /// </summary>
    protected EvidenceAttachment()
    {
        FileUrl = string.Empty;
        Description = string.Empty;
    }

    /// <param name="fileUrl">The URL of the evidence file.</param>
    /// <param name="description">What the evidence shows.</param>
    public EvidenceAttachment(string fileUrl, string description)
    {
        if (string.IsNullOrWhiteSpace(fileUrl)) throw new ArgumentException(FileUrlNotBlankMessageKey);
        FileUrl = fileUrl;
        Description = description ?? string.Empty;
    }

    public int Id { get; private set; }
    public string FileUrl { get; private set; }
    public string Description { get; private set; }
}
