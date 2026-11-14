using IngesCompanyCCPlatform.API.Manufacturing.Application.CommandServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.Internal.CommandServices;

/// <summary>
///     Command service of the products.
/// </summary>
public class ProductCommandService(IProductRepository productRepository, IUnitOfWork unitOfWork)
    : IProductCommandService
{
    /// <inheritdoc />
    public async Task<Result<Product>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        if (await productRepository.ExistsByCodeAsync(command.Code, cancellationToken))
            return Result<Product>.Failure(ProductErrors.DuplicateCode, ManufacturingError.DuplicateProductCode);

        var product = new Product(command);
        await productRepository.AddAsync(product, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Product>.Success(product);
    }

    /// <inheritdoc />
    public async Task<Result<Product>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await productRepository.FindByIdAsync(command.ProductId, cancellationToken);
        if (product is null)
            return Result<Product>.Failure(ProductErrors.NotFoundById(command.ProductId),
                ManufacturingError.ProductNotFound);

        product.Update(command);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Product>.Success(product);
    }
}
