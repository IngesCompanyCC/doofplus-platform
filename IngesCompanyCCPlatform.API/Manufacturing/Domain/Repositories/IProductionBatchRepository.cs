using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;

/// <summary>
///     Repository contract for the ProductionBatch aggregate root.
/// </summary>
public interface IProductionBatchRepository : IBaseRepository<ProductionBatch>
{
    /// <summary>
    ///     Finds a production batch by its unique business batch number.
    /// </summary>
    /// <param name="batchNumber">The batch number to look for.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The matching production batch, or null if not found.</returns>
    Task<ProductionBatch?> FindByBatchNumberAsync(BatchNumber batchNumber, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Checks whether a production batch with the specified batch number already exists.
    /// </summary>
    /// <param name="batchNumber">The batch number to check.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if a batch exists with the given number; otherwise, false.</returns>
    Task<bool> ExistsByBatchNumberAsync(BatchNumber batchNumber, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds all production batches with the specified lifecycle status.
    /// </summary>
    /// <param name="status">The batch status to filter by.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of matching production batches.</returns>
    Task<IEnumerable<ProductionBatch>> FindByStatusAsync(BatchStatus status, CancellationToken cancellationToken = default);
}
