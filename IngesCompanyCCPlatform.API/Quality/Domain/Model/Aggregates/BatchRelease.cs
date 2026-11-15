using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     The request to release a production batch and the signed decision of Quality.
/// </summary>
public partial class BatchRelease : AggregateRoot
{
    private const string BatchCodeNotBlankMessageKey = "quality.batchRelease.error.batchCode.notBlank";
    private const string RequestedByNotBlankMessageKey = "quality.batchRelease.error.requestedBy.notBlank";
    private const string DecisionNotBlankMessageKey = "quality.batchRelease.error.decision.notBlank";
    private const string SignatureReferenceNotBlankMessageKey = "quality.batchRelease.error.signatureReference.notBlank";
    private const string SignedByNotBlankMessageKey = "quality.batchRelease.error.signedBy.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected BatchRelease()
    {
        BatchCode = string.Empty;
        RequestedBy = string.Empty;
        Decision = string.Empty;
        SignatureReference = string.Empty;
        SignedBy = string.Empty;
    }

    /// <summary>
    ///     Creates the batch release from its command.
    /// </summary>
    /// <param name="command">The data of the batch release.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public BatchRelease(CreateBatchReleaseCommand command) : this()
    {
        BatchId = command.BatchId;
        if (string.IsNullOrWhiteSpace(command.BatchCode)) throw new ArgumentException(BatchCodeNotBlankMessageKey);
        BatchCode = command.BatchCode.Trim();
        if (string.IsNullOrWhiteSpace(command.RequestedBy)) throw new ArgumentException(RequestedByNotBlankMessageKey);
        RequestedBy = command.RequestedBy.Trim();
        Status = EBatchReleaseStatus.Pending;
    }

    /// <summary>
    ///     The id of the batch release.
    /// </summary>
    public BatchReleaseId Id { get; }

    /// <summary>
    ///     The id of the production batch.
    /// </summary>
    public int BatchId { get; private set; }
    /// <summary>
    ///     The code of the production batch, for example B-26043.
    /// </summary>
    public string BatchCode { get; private set; }
    /// <summary>
    ///     Who requested the release.
    /// </summary>
    public string RequestedBy { get; private set; }
    /// <summary>
    ///     Where the release is.
    /// </summary>
    public EBatchReleaseStatus Status { get; private set; }
    /// <summary>
    ///     The decision of Quality and its reason.
    /// </summary>
    public string Decision { get; private set; }
    /// <summary>
    ///     The reference of the electronic signature.
    /// </summary>
    public string SignatureReference { get; private set; }
    /// <summary>
    ///     Who signed the decision.
    /// </summary>
    public string SignedBy { get; private set; }
    /// <summary>
    ///     When the decision was signed (UTC).
    /// </summary>
    public DateTime? SignedAt { get; private set; }

    /// <summary>
    ///     Approves a pending release with the electronic signature of Quality.
    /// </summary>
    /// <param name="command">The decision, the signature reference and who signs.</param>
    /// <returns><c>false</c> when the release is not pending.</returns>
    public bool Approve(ApproveBatchReleaseCommand command)
    {
        if (Status != EBatchReleaseStatus.Pending) return false;
        if (string.IsNullOrWhiteSpace(command.Decision)) throw new ArgumentException(DecisionNotBlankMessageKey);
        if (string.IsNullOrWhiteSpace(command.SignatureReference)) throw new ArgumentException(SignatureReferenceNotBlankMessageKey);
        if (string.IsNullOrWhiteSpace(command.SignedBy)) throw new ArgumentException(SignedByNotBlankMessageKey);
        Decision = command.Decision.Trim();
        SignatureReference = command.SignatureReference.Trim();
        SignedBy = command.SignedBy.Trim();
        SignedAt = DateTime.UtcNow;
        Status = EBatchReleaseStatus.Approved;
        return true;
    }

    /// <summary>
    ///     Rejects a pending release.
    /// </summary>
    /// <param name="command">The reason and who signs.</param>
    /// <returns><c>false</c> when the release is not pending.</returns>
    public bool Reject(RejectBatchReleaseCommand command)
    {
        if (Status != EBatchReleaseStatus.Pending) return false;
        if (string.IsNullOrWhiteSpace(command.Decision)) throw new ArgumentException(DecisionNotBlankMessageKey);
        if (string.IsNullOrWhiteSpace(command.SignedBy)) throw new ArgumentException(SignedByNotBlankMessageKey);
        Decision = command.Decision.Trim();
        SignedBy = command.SignedBy.Trim();
        SignedAt = DateTime.UtcNow;
        Status = EBatchReleaseStatus.Rejected;
        return true;
    }

    /// <summary>
    ///     Marks an approved release as released to distribution.
    /// </summary>
    public bool Release()
    {
        if (Status != EBatchReleaseStatus.Approved) return false;
        Status = EBatchReleaseStatus.Released;
        return true;
    }
}
