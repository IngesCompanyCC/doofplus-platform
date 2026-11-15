using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Command service of <see cref="TaskComment" />.
/// </summary>
public interface ITaskCommentCommandService
{
    /// <summary>
    ///     Creates a task comment.
    /// </summary>
    Task<Result<TaskComment>> Handle(CreateTaskCommentCommand command, CancellationToken cancellationToken);
}
