namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Response resource representing the review of a production batch.
/// </summary>
public record BatchReviewResource(int Id, int ProductionBatchId, string Status, DateTime QuarantineStartedAt, string? SampleCode, DateTime? EvaluatedAt, bool ReleaseBlocked, string? RejectionReason, IEnumerable<EvidenceAttachmentResource> Evidences, ReleaseCertificateResource? Certificate);
