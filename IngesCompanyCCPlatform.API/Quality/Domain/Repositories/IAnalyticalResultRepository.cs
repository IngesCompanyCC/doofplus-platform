using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Represents the analytical result repository of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IAnalyticalResultRepository : IBaseRepository<AnalyticalResult>
{
    /// <summary>
    ///     Lists the analytical results of a production batch.
    /// </summary>
    /// <param name="productionBatchId">The production batch.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The analytical results of the batch.</returns>
    Task<IReadOnlyCollection<AnalyticalResult>> FindByProductionBatchIdAsync(ProductionBatchId productionBatchId,
        CancellationToken cancellationToken = default);
}
