using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="TraceabilityGap" />.
/// </summary>
public class TraceabilityGapRepository(AppDbContext context) : BaseRepository<TraceabilityGap>(context), ITraceabilityGapRepository
{
    /// <inheritdoc />
    public new async Task<TraceabilityGap?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var traceabilityGapId = new TraceabilityGapId(id);
        return await Context.Set<TraceabilityGap>().FirstOrDefaultAsync(traceabilityGap => traceabilityGap.Id == traceabilityGapId, cancellationToken);
    }

    /// <inheritdoc />
    public new async Task<TraceabilityGap?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var traceabilityGapId = new TraceabilityGapId(id);
        return await Context.Set<TraceabilityGap>()
            .AsNoTracking()
            .FirstOrDefaultAsync(traceabilityGap => traceabilityGap.Id == traceabilityGapId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TraceabilityGap>> FindAllByBatchIdAsNoTrackingAsync(int batchId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TraceabilityGap>()
            .AsNoTracking()
            .Where(traceabilityGap => traceabilityGap.BatchId == batchId)
            .ToListAsync(cancellationToken);
    }
}
