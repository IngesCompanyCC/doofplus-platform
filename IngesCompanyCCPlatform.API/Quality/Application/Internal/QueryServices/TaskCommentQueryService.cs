using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="TaskComment" />.
/// </summary>
public class TaskCommentQueryService(ITaskCommentRepository taskCommentRepository) : ITaskCommentQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<TaskComment>> Handle(GetAllTaskCommentsQuery query, CancellationToken cancellationToken)
    {
        return await taskCommentRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TaskComment?> Handle(GetTaskCommentByIdQuery query, CancellationToken cancellationToken)
    {
        return await taskCommentRepository.FindByIdAsNoTrackingAsync(query.TaskCommentId, cancellationToken);
    }
    /// <inheritdoc />
    public async Task<IEnumerable<TaskComment>> Handle(GetAllTaskCommentsByTaskIdQuery query, CancellationToken cancellationToken)
    {
        return await taskCommentRepository.FindAllByTaskIdAsNoTrackingAsync(query.TaskId, cancellationToken);
    }
}
