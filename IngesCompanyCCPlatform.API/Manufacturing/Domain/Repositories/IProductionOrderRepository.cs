using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;

/// <summary>
///     Repository of the production orders.
/// </summary>
public interface IProductionOrderRepository : IBaseRepository<ProductionOrder>
{
    /// <summary>
    ///     Checks whether an order already has the code.
    /// </summary>
    Task<bool> ExistsByCodeAsync(ProductionOrderCode code, CancellationToken cancellationToken = default);
}
