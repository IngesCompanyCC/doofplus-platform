using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Command service of <see cref="CollaborationTask" />.
/// </summary>
public interface ICollaborationTaskCommandService
{
    /// <summary>
    ///     Creates a collaboration task.
    /// </summary>
    Task<Result<CollaborationTask>> Handle(CreateCollaborationTaskCommand command, CancellationToken cancellationToken);
}
