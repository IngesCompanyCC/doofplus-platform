using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="AnalyticalResult" />.
/// </summary>
public class AnalyticalResultRepository(AppDbContext context) : BaseRepository<AnalyticalResult>(context), IAnalyticalResultRepository
{
    /// <inheritdoc />
    public new async Task<AnalyticalResult?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var analyticalResultId = new AnalyticalResultId(id);
        return await Context.Set<AnalyticalResult>().FirstOrDefaultAsync(analyticalResult => analyticalResult.Id == analyticalResultId, cancellationToken);
    }

    /// <inheritdoc />
    public new async Task<AnalyticalResult?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var analyticalResultId = new AnalyticalResultId(id);
        return await Context.Set<AnalyticalResult>()
            .AsNoTracking()
            .FirstOrDefaultAsync(analyticalResult => analyticalResult.Id == analyticalResultId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AnalyticalResult>> FindAllByBatchCodeAsNoTrackingAsync(string batchCode, CancellationToken cancellationToken = default)
    {
        return await Context.Set<AnalyticalResult>()
            .AsNoTracking()
            .Where(analyticalResult => analyticalResult.BatchCode == batchCode)
            .ToListAsync(cancellationToken);
    }
}
