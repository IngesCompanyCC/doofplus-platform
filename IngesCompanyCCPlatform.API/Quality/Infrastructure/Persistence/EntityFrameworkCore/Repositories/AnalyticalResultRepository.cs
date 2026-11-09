using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Represents the analytical result repository of the Quality &amp; Compliance bounded context.
/// </summary>
/// <param name="context">The <see cref="AppDbContext" /> to use.</param>
public class AnalyticalResultRepository(AppDbContext context)
    : BaseRepository<AnalyticalResult>(context), IAnalyticalResultRepository
{
    // <inheritdoc />
    public async Task<IReadOnlyCollection<AnalyticalResult>> FindByProductionBatchIdAsync(
        ProductionBatchId productionBatchId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<AnalyticalResult>()
            .AsNoTracking()
            .Where(result => result.ProductionBatchId == productionBatchId)
            .ToListAsync(cancellationToken);
    }

    // <inheritdoc />
    public new async Task<AnalyticalResult?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var resultId = new AnalyticalResultId(id);
        return await Context.Set<AnalyticalResult>()
            .FirstOrDefaultAsync(result => result.Id == resultId, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<AnalyticalResult?> FindByIdAsNoTrackingAsync(int id,
        CancellationToken cancellationToken = default)
    {
        var resultId = new AnalyticalResultId(id);
        return await Context.Set<AnalyticalResult>()
            .AsNoTracking()
            .FirstOrDefaultAsync(result => result.Id == resultId, cancellationToken);
    }
}
