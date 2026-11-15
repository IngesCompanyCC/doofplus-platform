using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="CollaborationTask" />.
/// </summary>
public class CollaborationTaskRepository(AppDbContext context) : BaseRepository<CollaborationTask>(context), ICollaborationTaskRepository
{
    /// <inheritdoc />
    public new async Task<CollaborationTask?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var collaborationTaskId = new CollaborationTaskId(id);
        return await Context.Set<CollaborationTask>().FirstOrDefaultAsync(collaborationTask => collaborationTask.Id == collaborationTaskId, cancellationToken);
    }

    /// <inheritdoc />
    public new async Task<CollaborationTask?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var collaborationTaskId = new CollaborationTaskId(id);
        return await Context.Set<CollaborationTask>()
            .AsNoTracking()
            .FirstOrDefaultAsync(collaborationTask => collaborationTask.Id == collaborationTaskId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CollaborationTask>> FindAllByRecordCodeAsNoTrackingAsync(string recordCode, CancellationToken cancellationToken = default)
    {
        return await Context.Set<CollaborationTask>()
            .AsNoTracking()
            .Where(collaborationTask => collaborationTask.RecordCode == recordCode)
            .ToListAsync(cancellationToken);
    }
}
