using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Represents the batch review repository of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IBatchReviewRepository : IBaseRepository<BatchReview>
{
    /// <summary>
    ///     Finds the review of a production batch.
    /// </summary>
    /// <param name="productionBatchId">The production batch.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The batch review, or <c>null</c> when the batch has none.</returns>
    Task<BatchReview?> FindByProductionBatchIdAsync(ProductionBatchId productionBatchId,
        CancellationToken cancellationToken = default);
}
