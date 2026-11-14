using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Manufacturing.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     ProductionBatch repository implementation using Entity Framework Core.
/// </summary>
/// <param name="context">The application database context.</param>
public class ProductionBatchRepository(AppDbContext context)
    : BaseRepository<ProductionBatch>(context), IProductionBatchRepository
{
    /// <inheritdoc />
    public async Task<ProductionBatch?> FindByBatchNumberAsync(
        BatchNumber batchNumber,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<ProductionBatch>()
            .Include(batch => batch.Incidents)
            .Include(batch => batch.Consumptions)
            .Include(batch => batch.Events)
            .FirstOrDefaultAsync(batch => batch.BatchNumber.Value == batchNumber.Value, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByBatchNumberAsync(
        BatchNumber batchNumber,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<ProductionBatch>()
            .AnyAsync(batch => batch.BatchNumber.Value == batchNumber.Value, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ProductionBatch>> FindByStatusAsync(
        BatchStatus status,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<ProductionBatch>()
            .Include(batch => batch.Incidents)
            .Include(batch => batch.Consumptions)
            .Include(batch => batch.Events)
            .Where(batch => batch.Status == status)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<ProductionBatch?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<ProductionBatch>()
            .Include(batch => batch.Incidents)
            .Include(batch => batch.Consumptions)
            .Include(batch => batch.Events)
            .FirstOrDefaultAsync(batch => batch.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<ProductionBatch?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<ProductionBatch>()
            .AsNoTracking()
            .AsSplitQuery()
            .Include(batch => batch.Incidents)
            .Include(batch => batch.Consumptions)
            .Include(batch => batch.Events)
            .FirstOrDefaultAsync(batch => batch.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<IEnumerable<ProductionBatch>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<ProductionBatch>()
            .Include(batch => batch.Incidents)
            .Include(batch => batch.Consumptions)
            .Include(batch => batch.Events)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<IEnumerable<ProductionBatch>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<ProductionBatch>()
            .AsNoTracking()
            .AsSplitQuery()
            .Include(batch => batch.Incidents)
            .Include(batch => batch.Consumptions)
            .Include(batch => batch.Events)
            .ToListAsync(cancellationToken);
    }
}
