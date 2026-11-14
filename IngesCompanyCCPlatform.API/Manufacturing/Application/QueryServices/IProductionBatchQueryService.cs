using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.QueryServices;

/// <summary>
///     Query service contract for ProductionBatch operations in the Manufacturing bounded context.
/// </summary>
public interface IProductionBatchQueryService
{
    /// <summary>
    ///     Handles retrieving all production batches.
    /// </summary>
    /// <param name="query">The get all batches query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of all production batches.</returns>
    Task<IEnumerable<ProductionBatch>> Handle(GetAllBatchesQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles retrieving a production batch by its database identifier.
    /// </summary>
    /// <param name="query">The get batch by id query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The matching production batch, or null if not found.</returns>
    Task<ProductionBatch?> Handle(GetBatchByIdQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles retrieving a production batch by its business batch number.
    /// </summary>
    /// <param name="query">The get batch by number query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The matching production batch, or null if not found.</returns>
    Task<ProductionBatch?> Handle(GetBatchByNumberQuery query, CancellationToken cancellationToken = default);
}
