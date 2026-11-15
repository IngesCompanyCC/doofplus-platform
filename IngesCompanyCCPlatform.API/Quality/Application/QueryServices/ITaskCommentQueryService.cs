using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Query service of <see cref="TaskComment" />.
/// </summary>
public interface ITaskCommentQueryService
{
    /// <summary>
    ///     Gets the task comments, filtered by record code when it is sent.
    /// </summary>
    Task<IEnumerable<TaskComment>> Handle(GetAllTaskCommentsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a task comment by its id, or <c>null</c>.
    /// </summary>
    Task<TaskComment?> Handle(GetTaskCommentByIdQuery query, CancellationToken cancellationToken);
}
