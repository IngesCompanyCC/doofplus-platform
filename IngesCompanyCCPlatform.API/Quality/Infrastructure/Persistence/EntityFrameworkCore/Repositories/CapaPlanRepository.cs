using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="CapaPlan" />.
/// </summary>
public class CapaPlanRepository(AppDbContext context) : BaseRepository<CapaPlan>(context), ICapaPlanRepository
{
    /// <inheritdoc />
    public new async Task<CapaPlan?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var capaPlanId = new CapaPlanId(id);
        return await Context.Set<CapaPlan>().FirstOrDefaultAsync(capaPlan => capaPlan.Id == capaPlanId, cancellationToken);
    }

    /// <inheritdoc />
    public new async Task<CapaPlan?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var capaPlanId = new CapaPlanId(id);
        return await Context.Set<CapaPlan>()
            .AsNoTracking()
            .FirstOrDefaultAsync(capaPlan => capaPlan.Id == capaPlanId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<CapaPlan>().AnyAsync(capaPlan => capaPlan.Code == code, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CapaPlan>> FindAllBySourceDeviationAsNoTrackingAsync(string sourceDeviation, CancellationToken cancellationToken = default)
    {
        return await Context.Set<CapaPlan>()
            .AsNoTracking()
            .Where(capaPlan => capaPlan.SourceDeviation == sourceDeviation)
            .ToListAsync(cancellationToken);
    }
}
