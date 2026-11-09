using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to evaluate a batch against its analytical results and deviations.
/// </summary>
public record EvaluateBatchCommand(BatchReviewId BatchReviewId);
