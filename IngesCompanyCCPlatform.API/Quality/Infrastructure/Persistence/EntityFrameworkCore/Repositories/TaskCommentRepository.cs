using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="TaskComment" />.
/// </summary>
public class TaskCommentRepository(AppDbContext context) : BaseRepository<TaskComment>(context), ITaskCommentRepository
{
    /// <inheritdoc />
    public new async Task<TaskComment?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var taskCommentId = new TaskCommentId(id);
        return await Context.Set<TaskComment>().FirstOrDefaultAsync(taskComment => taskComment.Id == taskCommentId, cancellationToken);
    }

    /// <inheritdoc />
    public new async Task<TaskComment?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var taskCommentId = new TaskCommentId(id);
        return await Context.Set<TaskComment>()
            .AsNoTracking()
            .FirstOrDefaultAsync(taskComment => taskComment.Id == taskCommentId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TaskComment>> FindAllByRecordCodeAsNoTrackingAsync(string recordCode, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TaskComment>()
            .AsNoTracking()
            .Where(taskComment => taskComment.RecordCode == recordCode)
            .ToListAsync(cancellationToken);
    }
}
