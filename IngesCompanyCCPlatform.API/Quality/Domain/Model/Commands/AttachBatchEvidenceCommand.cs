using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to attach an evidence file to the review of a batch.
/// </summary>
public record AttachBatchEvidenceCommand(BatchReviewId BatchReviewId, string FileUrl, string Description);
