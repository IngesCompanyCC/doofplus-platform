using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="BatchRelease" />.
/// </summary>
public class BatchReleaseQueryService(IBatchReleaseRepository batchReleaseRepository) : IBatchReleaseQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<BatchRelease>> Handle(GetAllBatchReleasesQuery query, CancellationToken cancellationToken)
    {
        return await batchReleaseRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<BatchRelease?> Handle(GetBatchReleaseByIdQuery query, CancellationToken cancellationToken)
    {
        return await batchReleaseRepository.FindByIdAsNoTrackingAsync(query.BatchReleaseId, cancellationToken);
    }
    /// <inheritdoc />
    public async Task<IEnumerable<BatchRelease>> Handle(GetAllBatchReleasesByBatchIdQuery query, CancellationToken cancellationToken)
    {
        return await batchReleaseRepository.FindAllByBatchIdAsNoTrackingAsync(query.BatchId, cancellationToken);
    }
}
