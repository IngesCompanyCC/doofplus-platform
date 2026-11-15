using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="AnalyticalResult" />.
/// </summary>
public interface IAnalyticalResultRepository : IBaseRepository<AnalyticalResult>
{
    /// <summary>
    ///     Gets the analytical results with the batch code.
    /// </summary>
    Task<IEnumerable<AnalyticalResult>> FindAllByBatchCodeAsNoTrackingAsync(string batchCode, CancellationToken cancellationToken = default);
}
