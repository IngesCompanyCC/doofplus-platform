using IngesCompanyCCPlatform.API.Manufacturing.Application.QueryServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.Internal.QueryServices;

/// <summary>
///     Production batch query service implementation.
/// </summary>
public class ProductionBatchQueryService(IProductionBatchRepository productionBatchRepository)
    : IProductionBatchQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<ProductionBatch>> Handle(
        GetAllBatchesQuery query,
        CancellationToken cancellationToken = default)
    {
        return await productionBatchRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ProductionBatch?> Handle(
        GetBatchByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await productionBatchRepository.FindByIdAsNoTrackingAsync(query.Id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ProductionBatch?> Handle(
        GetBatchByNumberQuery query,
        CancellationToken cancellationToken = default)
    {
        return await productionBatchRepository.FindByBatchNumberAsync(query.BatchNumber, cancellationToken);
    }
}
