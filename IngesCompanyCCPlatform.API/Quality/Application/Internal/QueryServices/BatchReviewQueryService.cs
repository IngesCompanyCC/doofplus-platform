using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Implements the batch review query service of the Quality &amp; Compliance bounded context.
/// </summary>
public class BatchReviewQueryService(IBatchReviewRepository batchReviewRepository) : IBatchReviewQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<BatchReview>> Handle(GetAllBatchReviewsQuery query,
        CancellationToken cancellationToken)
    {
        return await batchReviewRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<BatchReview?> Handle(GetBatchReviewByIdQuery query, CancellationToken cancellationToken)
    {
        return await batchReviewRepository.FindByIdAsNoTrackingAsync(query.BatchReviewId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<BatchReview?> Handle(GetBatchReviewByProductionBatchIdQuery query,
        CancellationToken cancellationToken)
    {
        return await batchReviewRepository.FindByProductionBatchIdAsync(query.ProductionBatchId, cancellationToken);
    }
}
