using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.QueryServices;

/// <summary>
///     Query service of the products.
/// </summary>
public interface IProductQueryService
{
    /// <summary>
    ///     Gets all the products.
    /// </summary>
    Task<IEnumerable<Product>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a product by its id, or null.
    /// </summary>
    Task<Product?> Handle(GetProductByIdQuery query, CancellationToken cancellationToken);
}
