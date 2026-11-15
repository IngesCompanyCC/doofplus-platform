namespace IngesCompanyCCPlatform.API.Quality.Domain.Model;

/// <summary>
///     Errors of the Quality &amp; Compliance bounded context.
/// </summary>
public enum QualityError
{
    None,
    QualityDocumentNotFound,
    DeviationNotFound,
    DuplicateDeviationCode,
    EvidenceNotFound,
    CapaPlanNotFound,
    DuplicateCapaPlanCode,
    CapaActionNotFound,
    AnalyticalResultNotFound,
    AuditNotFound,
    DuplicateAuditCode,
    AuditFindingNotFound,
    AuditEventNotFound,
    RegulatoryReportNotFound,
    DuplicateRegulatoryReportCode,
    CollaborationTaskNotFound,
    TaskCommentNotFound,
    TraceabilityGapNotFound,
    EvidenceReviewed,
    BatchReleaseNotFound,
    BatchReleaseAlreadyRequested,
    BatchReleaseStateConflict
}
