using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="BatchRelease" />.
/// </summary>
public interface IBatchReleaseRepository : IBaseRepository<BatchRelease>
{
    /// <summary>
    ///     Checks whether a batch already has a release request.
    /// </summary>
    Task<bool> ExistsByBatchIdAsync(int batchId, CancellationToken cancellationToken = default);
    /// <summary>
    ///     Gets the batch releases filtered by batch id.
    /// </summary>
    Task<IEnumerable<BatchRelease>> FindAllByBatchIdAsNoTrackingAsync(int batchId, CancellationToken cancellationToken = default);
}
