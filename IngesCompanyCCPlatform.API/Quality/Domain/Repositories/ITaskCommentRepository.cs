using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="TaskComment" />.
/// </summary>
public interface ITaskCommentRepository : IBaseRepository<TaskComment>
{
    /// <summary>
    ///     Gets the task comments with the record code.
    /// </summary>
    Task<IEnumerable<TaskComment>> FindAllByRecordCodeAsNoTrackingAsync(string recordCode, CancellationToken cancellationToken = default);
}
