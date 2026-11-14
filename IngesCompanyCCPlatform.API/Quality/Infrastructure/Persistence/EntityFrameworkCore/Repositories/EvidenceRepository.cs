using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="Evidence" />.
/// </summary>
public class EvidenceRepository(AppDbContext context) : BaseRepository<Evidence>(context), IEvidenceRepository
{
    /// <inheritdoc />
    public new async Task<Evidence?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var evidenceId = new EvidenceId(id);
        return await Context.Set<Evidence>().FirstOrDefaultAsync(evidence => evidence.Id == evidenceId, cancellationToken);
    }

    /// <inheritdoc />
    public new async Task<Evidence?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var evidenceId = new EvidenceId(id);
        return await Context.Set<Evidence>()
            .AsNoTracking()
            .FirstOrDefaultAsync(evidence => evidence.Id == evidenceId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Evidence>> FindAllByDeviationIdAsNoTrackingAsync(int deviationId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Evidence>()
            .AsNoTracking()
            .Where(evidence => evidence.DeviationId == deviationId)
            .ToListAsync(cancellationToken);
    }
}
