using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to send the current version of a quality document to review.
/// </summary>
public record SubmitQualityDocumentForReviewCommand(QualityDocumentId QualityDocumentId);
