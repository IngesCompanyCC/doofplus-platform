using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;

/// <summary>
///     Domain errors of the Quality &amp; Compliance bounded context.
/// </summary>
public static class QualityErrors
{
    public static Error InvalidData(string message) =>
        Error.Validation("Quality.InvalidData", message);

    public static Error InvalidState(string message) =>
        Error.Conflict("Quality.InvalidState", message);

    public static Error QualityDocumentNotFound(int id) =>
        Error.NotFound("Quality.QualityDocumentNotFound", $"Quality document not found for id {id}.");

    public static Error DuplicateQualityDocumentCode(string code) =>
        Error.Conflict("Quality.DuplicateQualityDocumentCode", $"A quality document with the code '{code}' already exists.");

    public static Error MaterialApprovalNotFound(int id) =>
        Error.NotFound("Quality.MaterialApprovalNotFound", $"Material approval not found for id {id}.");

    public static Error MaterialApprovalAlreadyRequested(int rawMaterialLotId) =>
        Error.Conflict("Quality.MaterialApprovalAlreadyRequested",
            $"The raw material lot {rawMaterialLotId} already has a material approval.");

    public static Error AnalyticalResultNotFound(int id) =>
        Error.NotFound("Quality.AnalyticalResultNotFound", $"Analytical result not found for id {id}.");

    public static Error BatchReviewNotFound(int id) =>
        Error.NotFound("Quality.BatchReviewNotFound", $"Batch review not found for id {id}.");

    public static Error BatchReviewAlreadyStarted(int productionBatchId) =>
        Error.Conflict("Quality.BatchReviewAlreadyStarted",
            $"The production batch {productionBatchId} is already under review.");

    public static Error DeviationNotFound(int id) =>
        Error.NotFound("Quality.DeviationNotFound", $"Deviation not found for id {id}.");

    public static Error DuplicateDeviationCode(string code) =>
        Error.Conflict("Quality.DuplicateDeviationCode", $"A deviation with the code '{code}' already exists.");

    public static Error AuditNotFound(int id) =>
        Error.NotFound("Quality.AuditNotFound", $"Audit not found for id {id}.");
}
