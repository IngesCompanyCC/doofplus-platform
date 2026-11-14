using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="CollaborationTask" />.
/// </summary>
public class CollaborationTaskQueryService(ICollaborationTaskRepository collaborationTaskRepository) : ICollaborationTaskQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<CollaborationTask>> Handle(GetAllCollaborationTasksQuery query, CancellationToken cancellationToken)
    {
        return await collaborationTaskRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<CollaborationTask?> Handle(GetCollaborationTaskByIdQuery query, CancellationToken cancellationToken)
    {
        return await collaborationTaskRepository.FindByIdAsNoTrackingAsync(query.CollaborationTaskId, cancellationToken);
    }
}
