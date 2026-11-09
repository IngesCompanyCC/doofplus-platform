using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a batch review by its id.
/// </summary>
public record GetBatchReviewByIdQuery(BatchReviewId BatchReviewId);
