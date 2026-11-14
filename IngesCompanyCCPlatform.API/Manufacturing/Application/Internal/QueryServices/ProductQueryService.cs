using IngesCompanyCCPlatform.API.Manufacturing.Application.QueryServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="Product" />.
/// </summary>
public class ProductQueryService(IProductRepository productRepository) : IProductQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        return await productRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Product?> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        return await productRepository.FindByIdAsNoTrackingAsync(query.ProductId, cancellationToken);
    }
}
