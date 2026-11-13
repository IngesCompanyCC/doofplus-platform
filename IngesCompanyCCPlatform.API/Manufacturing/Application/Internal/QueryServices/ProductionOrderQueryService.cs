using IngesCompanyCCPlatform.API.Manufacturing.Application.QueryServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="ProductionOrder" />.
/// </summary>
public class ProductionOrderQueryService(IProductionOrderRepository productionOrderRepository) : IProductionOrderQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<ProductionOrder>> Handle(GetAllProductionOrdersQuery query, CancellationToken cancellationToken)
    {
        return await productionOrderRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ProductionOrder?> Handle(GetProductionOrderByIdQuery query, CancellationToken cancellationToken)
    {
        return await productionOrderRepository.FindByIdAsNoTrackingAsync(query.ProductionOrderId, cancellationToken);
    }
}
