using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.CommandServices;

/// <summary>
///     Command service of the products.
/// </summary>
public interface IProductCommandService
{
    /// <summary>
    ///     Registers a product; fails if the code already exists.
    /// </summary>
    Task<Result<Product>> Handle(CreateProductCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Updates a product; fails if it does not exist.
    /// </summary>
    Task<Result<Product>> Handle(UpdateProductCommand command, CancellationToken cancellationToken);
}
