using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Query service of <see cref="CollaborationTask" />.
/// </summary>
public interface ICollaborationTaskQueryService
{
    /// <summary>
    ///     Gets all the collaboration tasks.
    /// </summary>
    Task<IEnumerable<CollaborationTask>> Handle(GetAllCollaborationTasksQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a collaboration task by its id, or <c>null</c>.
    /// </summary>
    Task<CollaborationTask?> Handle(GetCollaborationTaskByIdQuery query, CancellationToken cancellationToken);
}
