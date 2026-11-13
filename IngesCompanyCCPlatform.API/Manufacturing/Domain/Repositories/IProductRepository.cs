using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;

/// <summary>
///     Repository of the products.
/// </summary>
public interface IProductRepository : IBaseRepository<Product>
{
    /// <summary>
    ///     Checks whether a product already has the code.
    /// </summary>
    Task<bool> ExistsByCodeAsync(ProductCode code, CancellationToken cancellationToken = default);
}
