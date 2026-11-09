using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to reject a batch under review.
/// </summary>
public record RejectBatchCommand(BatchReviewId BatchReviewId, string Reason, ElectronicSignatureId SignatureId);
