namespace IngesCompanyCCPlatform.API.Quality.Domain.Model;

/// <summary>
///     Errors of the Quality &amp; Compliance bounded context. Each case has its title in <c>ErrorMessages</c>
///     (<c>QualityError.&lt;Case&gt;</c>) and its HTTP status in <c>QualityActionResultAssembler</c>.
/// </summary>
public enum QualityError
{
    /// <summary>No error.</summary>
    None,

    /// <summary>Quality document not found.</summary>
    QualityDocumentNotFound,

    /// <summary>Duplicate quality document code.</summary>
    DuplicateQualityDocumentCode,

    /// <summary>Deviation not found.</summary>
    DeviationNotFound,

    /// <summary>Duplicate deviation code.</summary>
    DuplicateDeviationCode,

    /// <summary>Evidence not found.</summary>
    EvidenceNotFound,

    /// <summary>Capa plan not found.</summary>
    CapaPlanNotFound,

    /// <summary>Capa action not found.</summary>
    CapaActionNotFound,

    /// <summary>Batch release not found.</summary>
    BatchReleaseNotFound,

    /// <summary>Batch release already requested.</summary>
    BatchReleaseAlreadyRequested,

    /// <summary>Batch release state conflict.</summary>
    BatchReleaseStateConflict,

    /// <summary>Analytical result not found.</summary>
    AnalyticalResultNotFound,

    /// <summary>Audit not found.</summary>
    AuditNotFound,

    /// <summary>Duplicate audit code.</summary>
    DuplicateAuditCode,

    /// <summary>Audit finding not found.</summary>
    AuditFindingNotFound,

    /// <summary>Regulatory report not found.</summary>
    RegulatoryReportNotFound,

    /// <summary>Duplicate regulatory report code.</summary>
    DuplicateRegulatoryReportCode,

    /// <summary>Collaboration task not found.</summary>
    CollaborationTaskNotFound,

    /// <summary>Task comment not found.</summary>
    TaskCommentNotFound,

    /// <summary>Traceability gap not found.</summary>
    TraceabilityGapNotFound,

    /// <summary>Audit event not found.</summary>
    AuditEventNotFound
}
