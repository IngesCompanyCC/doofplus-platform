namespace IngesCompanyCCPlatform.API.Quality.Domain.Model;

/// <summary>
///     Represents the error types of the Quality &amp; Compliance bounded context.
/// </summary>
/// <remarks>
///     Each case has its localized title in <c>ErrorMessages</c> with the key <c>QualityError.&lt;Case&gt;</c>.
/// </remarks>
public enum QualityError
{
    None,
    InvalidData,
    InvalidState,
    QualityDocumentNotFound,
    DuplicateQualityDocumentCode,
    MaterialApprovalNotFound,
    MaterialApprovalAlreadyRequested,
    AnalyticalResultNotFound,
    BatchReviewNotFound,
    BatchReviewAlreadyStarted,
    DeviationNotFound,
    DuplicateDeviationCode,
    AuditNotFound,
    RegulatoryReportNotFound
}
