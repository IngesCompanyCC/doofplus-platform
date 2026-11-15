using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="CollaborationTask" />.
/// </summary>
public interface ICollaborationTaskRepository : IBaseRepository<CollaborationTask>
{
    /// <summary>
    ///     Gets the collaboration tasks with the record code.
    /// </summary>
    Task<IEnumerable<CollaborationTask>> FindAllByRecordCodeAsNoTrackingAsync(string recordCode, CancellationToken cancellationToken = default);
}
