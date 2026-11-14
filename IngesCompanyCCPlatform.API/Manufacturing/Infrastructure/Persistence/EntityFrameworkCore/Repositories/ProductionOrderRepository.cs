using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Manufacturing.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="ProductionOrder" />.
/// </summary>
public class ProductionOrderRepository(AppDbContext context) : BaseRepository<ProductionOrder>(context), IProductionOrderRepository
{
    /// <inheritdoc />
    public override async Task<ProductionOrder?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var orderId = new ProductionOrderId(id);
        return await Context.Set<ProductionOrder>().Include(order => order.Operations).FirstOrDefaultAsync(order => order.Id == orderId, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<ProductionOrder?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var orderId = new ProductionOrderId(id);
        return await Context.Set<ProductionOrder>().Include(order => order.Operations).AsNoTracking().FirstOrDefaultAsync(order => order.Id == orderId, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<IEnumerable<ProductionOrder>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<ProductionOrder>().Include(order => order.Operations).AsNoTracking().ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByCodeAsync(ProductionOrderCode code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<ProductionOrder>().AnyAsync(order => order.Code == code, cancellationToken);
    }
}
