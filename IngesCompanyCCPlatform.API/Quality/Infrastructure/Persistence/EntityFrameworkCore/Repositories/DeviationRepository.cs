using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="Deviation" />.
/// </summary>
public class DeviationRepository(AppDbContext context) : BaseRepository<Deviation>(context), IDeviationRepository
{
    /// <inheritdoc />
    public new async Task<Deviation?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var deviationId = new DeviationId(id);
        return await Context.Set<Deviation>().FirstOrDefaultAsync(deviation => deviation.Id == deviationId, cancellationToken);
    }

    /// <inheritdoc />
    public new async Task<Deviation?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var deviationId = new DeviationId(id);
        return await Context.Set<Deviation>()
            .AsNoTracking()
            .FirstOrDefaultAsync(deviation => deviation.Id == deviationId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Deviation>().AnyAsync(deviation => deviation.Code == code, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Deviation>> FindAllByBatchCodeAsNoTrackingAsync(string batchCode, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Deviation>()
            .AsNoTracking()
            .Where(deviation => deviation.BatchCode == batchCode)
            .ToListAsync(cancellationToken);
    }
}
