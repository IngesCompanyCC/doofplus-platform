using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Manufacturing.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="Product" />.
/// </summary>
public class ProductRepository(AppDbContext context) : BaseRepository<Product>(context), IProductRepository
{
    /// <inheritdoc />
    public override async Task<Product?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var productId = new ProductId(id);
        return await Context.Set<Product>().FirstOrDefaultAsync(product => product.Id == productId, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<Product?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var productId = new ProductId(id);
        return await Context.Set<Product>().AsNoTracking().FirstOrDefaultAsync(product => product.Id == productId, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<IEnumerable<Product>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<Product>().AsNoTracking().ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByCodeAsync(ProductCode code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Product>().AnyAsync(product => product.Code == code, cancellationToken);
    }
}
