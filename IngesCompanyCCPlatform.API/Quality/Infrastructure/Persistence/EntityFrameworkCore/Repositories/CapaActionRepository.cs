using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="CapaAction" />.
/// </summary>
public class CapaActionRepository(AppDbContext context) : BaseRepository<CapaAction>(context), ICapaActionRepository
{
    /// <inheritdoc />
    public new async Task<CapaAction?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var capaActionId = new CapaActionId(id);
        return await Context.Set<CapaAction>().FirstOrDefaultAsync(capaAction => capaAction.Id == capaActionId, cancellationToken);
    }

    /// <inheritdoc />
    public new async Task<CapaAction?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var capaActionId = new CapaActionId(id);
        return await Context.Set<CapaAction>()
            .AsNoTracking()
            .FirstOrDefaultAsync(capaAction => capaAction.Id == capaActionId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CapaAction>> FindAllByCapaCodeAsNoTrackingAsync(string capaCode, CancellationToken cancellationToken = default)
    {
        return await Context.Set<CapaAction>()
            .AsNoTracking()
            .Where(capaAction => capaAction.CapaCode == capaCode)
            .ToListAsync(cancellationToken);
    }
}
