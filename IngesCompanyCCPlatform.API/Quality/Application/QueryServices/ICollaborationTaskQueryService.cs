using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Query service of <see cref="CollaborationTask" />.
/// </summary>
public interface ICollaborationTaskQueryService
{
    /// <summary>
    ///     Gets the collaboration tasks, filtered by record code when it is sent.
    /// </summary>
    Task<IEnumerable<CollaborationTask>> Handle(GetAllCollaborationTasksQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a collaboration task by its id, or <c>null</c>.
    /// </summary>
    Task<CollaborationTask?> Handle(GetCollaborationTaskByIdQuery query, CancellationToken cancellationToken);
}
