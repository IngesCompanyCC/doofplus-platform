using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Represents the deviation repository of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IDeviationRepository : IBaseRepository<Deviation>
{
    /// <summary>
    ///     Checks whether a deviation with the given code exists.
    /// </summary>
    /// <param name="code">The code of the deviation.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><c>true</c> when a deviation already uses the code.</returns>
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Lists the deviations of a production batch.
    /// </summary>
    /// <param name="productionBatchId">The production batch.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The deviations of the batch.</returns>
    Task<IReadOnlyCollection<Deviation>> FindByProductionBatchIdAsync(ProductionBatchId productionBatchId,
        CancellationToken cancellationToken = default);
}
