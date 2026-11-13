using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.QueryServices;

/// <summary>
///     Query service of the production orders.
/// </summary>
public interface IProductionOrderQueryService
{
    /// <summary>
    ///     Gets all the orders with their operations.
    /// </summary>
    Task<IEnumerable<ProductionOrder>> Handle(GetAllProductionOrdersQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets an order by its id, or null.
    /// </summary>
    Task<ProductionOrder?> Handle(GetProductionOrderByIdQuery query, CancellationToken cancellationToken);
}
