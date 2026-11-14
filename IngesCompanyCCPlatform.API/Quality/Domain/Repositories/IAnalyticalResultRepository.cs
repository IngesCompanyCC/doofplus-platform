using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="AnalyticalResult" />.
/// </summary>
public interface IAnalyticalResultRepository : IBaseRepository<AnalyticalResult>
{
    /// <summary>
    ///     Gets the analytical results filtered by batch id.
    /// </summary>
    Task<IEnumerable<AnalyticalResult>> FindAllByBatchIdAsNoTrackingAsync(int batchId, CancellationToken cancellationToken = default);
}
