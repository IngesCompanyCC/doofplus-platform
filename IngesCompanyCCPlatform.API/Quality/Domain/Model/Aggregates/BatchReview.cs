using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Entities;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Events;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Represents the BatchReview aggregate root within the Quality &amp; Compliance bounded context.
/// </summary>
/// <remarks>
///     Controls the quarantine, evaluation and release of a production batch. A batch with an
///     out-of-specification result and no closed deviation, or with an open deviation, cannot be released.
///     Manufacturing learns the verdict through <see cref="BatchReleaseDecidedEvent" />.
/// </remarks>
public partial class BatchReview : AggregateRoot
{
    private readonly List<EvidenceAttachment> _evidences = [];

    /// <summary>
    ///     Initializes a new instance of the <see cref="BatchReview" /> aggregate. Used by Entity Framework Core.
    /// </summary>
    protected BatchReview()
    {
    }

    /// <summary>
    ///     Puts a production batch in quarantine and opens its review.
    /// </summary>
    /// <param name="command">The <see cref="StartQuarantineCommand" /> with the batch.</param>
    public BatchReview(StartQuarantineCommand command) : this()
    {
        ProductionBatchId = command.ProductionBatchId;
        Status = EReviewStatus.InQuarantine;
        QuarantineStartedAt = DateTime.UtcNow;
    }

    public BatchReviewId Id { get; }
    public ProductionBatchId ProductionBatchId { get; private set; }
    public EReviewStatus Status { get; private set; }
    public DateTime QuarantineStartedAt { get; private set; }
    public string? SampleCode { get; private set; }
    public DateTime? EvaluatedAt { get; private set; }

    /// <summary>
    ///     Gets whether the last evaluation found a reason that blocks the release.
    /// </summary>
    public bool ReleaseBlocked { get; private set; }

    /// <summary>
    ///     Gets the reason of the rejection, or <c>null</c> when the batch is not rejected.
    /// </summary>
    public string? RejectionReason { get; private set; }

    public IReadOnlyCollection<EvidenceAttachment> Evidences => _evidences.AsReadOnly();

    /// <summary>
    ///     Gets the certificate issued with the verdict, or <c>null</c> while there is no verdict.
    /// </summary>
    public ReleaseCertificate? Certificate { get; private set; }

    private bool HasVerdict => Status is EReviewStatus.Released or EReviewStatus.Rejected;

    /// <summary>
    ///     Registers the sample taken from the batch in quarantine.
    /// </summary>
    /// <param name="sampleCode">The code of the sample.</param>
    public void RegisterSample(string sampleCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sampleCode);
        if (Status != EReviewStatus.InQuarantine)
            throw new InvalidOperationException("A sample can only be registered for a batch in quarantine.");
        SampleCode = sampleCode;
    }

    /// <summary>
    ///     Attaches an evidence file to the review.
    /// </summary>
    /// <param name="fileUrl">The URL of the evidence file.</param>
    /// <param name="description">What the evidence shows.</param>
    public void AttachEvidence(string fileUrl, string description)
    {
        if (HasVerdict)
            throw new InvalidOperationException("Evidence cannot be attached to a batch that already has a verdict.");
        _evidences.Add(new EvidenceAttachment(fileUrl, description));
    }

    /// <summary>
    ///     Evaluates the batch with its analytical results and its deviations.
    /// </summary>
    /// <param name="results">The analytical results of the batch.</param>
    /// <param name="hasOpenDeviations">Whether the batch has deviations that are not closed.</param>
    /// <param name="hasClosedDeviations">Whether the batch has closed deviations.</param>
    public void Evaluate(IReadOnlyCollection<AnalyticalResult> results, bool hasOpenDeviations, bool hasClosedDeviations)
    {
        if (HasVerdict)
            throw new InvalidOperationException("A batch that already has a verdict cannot be evaluated.");
        if (SampleCode is null)
            throw new InvalidOperationException("The batch sample must be registered before the evaluation.");
        if (results.Count == 0)
            throw new InvalidOperationException("The batch has no analytical results to evaluate.");

        var hasUnresolvedOutOfSpecification = results.Any(result => result.OutOfSpecification) && !hasClosedDeviations;
        ReleaseBlocked = hasOpenDeviations || hasUnresolvedOutOfSpecification;
        Status = EReviewStatus.UnderEvaluation;
        EvaluatedAt = DateTime.UtcNow;
    }

    /// <summary>
    ///     Releases an evaluated batch and issues its certificate.
    /// </summary>
    /// <param name="signatureId">The electronic signature of the Quality Manager.</param>
    /// <returns>The <see cref="ReleaseCertificate" /> of the batch.</returns>
    public ReleaseCertificate Release(ElectronicSignatureId signatureId)
    {
        if (Status != EReviewStatus.UnderEvaluation)
            throw new InvalidOperationException("Only an evaluated batch can be released.");
        if (ReleaseBlocked)
            throw new InvalidOperationException(
                "The batch cannot be released: it has an open deviation or an out-of-specification result without a closed deviation.");
        return IssueVerdict(EReleaseVerdict.Released, signatureId);
    }

    /// <summary>
    ///     Rejects the batch and issues its certificate.
    /// </summary>
    /// <param name="reason">Why the batch is rejected.</param>
    /// <param name="signatureId">The electronic signature of the Quality Manager.</param>
    public void Reject(string reason, ElectronicSignatureId signatureId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reason);
        if (HasVerdict)
            throw new InvalidOperationException("The batch already has a verdict.");
        RejectionReason = reason;
        IssueVerdict(EReleaseVerdict.Rejected, signatureId);
    }

    private ReleaseCertificate IssueVerdict(EReleaseVerdict verdict, ElectronicSignatureId signatureId)
    {
        var certificateNumber = $"RC-{ProductionBatchId}-{DateTime.UtcNow:yyyyMMddHHmmss}";
        Certificate = new ReleaseCertificate(certificateNumber, verdict, signatureId);
        Status = verdict == EReleaseVerdict.Released ? EReviewStatus.Released : EReviewStatus.Rejected;
        AddDomainEvent(new BatchReleaseDecidedEvent(ProductionBatchId, verdict, certificateNumber));
        return Certificate;
    }
}
