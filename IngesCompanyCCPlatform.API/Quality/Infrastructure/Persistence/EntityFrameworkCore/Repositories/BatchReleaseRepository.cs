using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="BatchRelease" />.
/// </summary>
public class BatchReleaseRepository(AppDbContext context) : BaseRepository<BatchRelease>(context), IBatchReleaseRepository
{
    /// <inheritdoc />
    public new async Task<BatchRelease?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var batchReleaseId = new BatchReleaseId(id);
        return await Context.Set<BatchRelease>().FirstOrDefaultAsync(batchRelease => batchRelease.Id == batchReleaseId, cancellationToken);
    }

    /// <inheritdoc />
    public new async Task<BatchRelease?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var batchReleaseId = new BatchReleaseId(id);
        return await Context.Set<BatchRelease>()
            .AsNoTracking()
            .FirstOrDefaultAsync(batchRelease => batchRelease.Id == batchReleaseId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByBatchIdAsync(int batchId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<BatchRelease>().AnyAsync(release => release.BatchId == batchId, cancellationToken);
    }
    /// <inheritdoc />
    public async Task<IEnumerable<BatchRelease>> FindAllByBatchIdAsNoTrackingAsync(int batchId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<BatchRelease>()
            .AsNoTracking()
            .Where(batchRelease => batchRelease.BatchId == batchId)
            .ToListAsync(cancellationToken);
    }
}
