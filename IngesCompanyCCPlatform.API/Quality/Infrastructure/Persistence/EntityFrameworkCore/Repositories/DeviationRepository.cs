using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Represents the deviation repository of the Quality &amp; Compliance bounded context.
/// </summary>
/// <remarks>
///     Every read loads the CAPA actions of the deviation.
/// </remarks>
/// <param name="context">The <see cref="AppDbContext" /> to use.</param>
public class DeviationRepository(AppDbContext context) : BaseRepository<Deviation>(context), IDeviationRepository
{
    private IQueryable<Deviation> Deviations => Context.Set<Deviation>().Include(deviation => deviation.Actions);

    // <inheritdoc />
    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Deviation>().AnyAsync(deviation => deviation.Code == code, cancellationToken);
    }

    // <inheritdoc />
    public async Task<IReadOnlyCollection<Deviation>> FindByProductionBatchIdAsync(ProductionBatchId productionBatchId,
        CancellationToken cancellationToken = default)
    {
        return await Deviations.AsNoTracking()
            .Where(deviation => deviation.ProductionBatchId == productionBatchId)
            .ToListAsync(cancellationToken);
    }

    // <inheritdoc />
    public new async Task<Deviation?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var deviationId = new DeviationId(id);
        return await Deviations.FirstOrDefaultAsync(deviation => deviation.Id == deviationId, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<Deviation?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var deviationId = new DeviationId(id);
        return await Deviations.AsNoTracking()
            .FirstOrDefaultAsync(deviation => deviation.Id == deviationId, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<IEnumerable<Deviation>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        return await Deviations.AsNoTracking().ToListAsync(cancellationToken);
    }
}
