using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.CommandServices;

/// <summary>
///     Command service of <see cref="TaskComment" />.
/// </summary>
public class TaskCommentCommandService(
    ITaskCommentRepository taskCommentRepository,
    IUnitOfWork unitOfWork) : ITaskCommentCommandService
{
    /// <inheritdoc />
    public async Task<Result<TaskComment>> Handle(CreateTaskCommentCommand command, CancellationToken cancellationToken)
    {
        var taskComment = new TaskComment(command);
        await taskCommentRepository.AddAsync(taskComment, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<TaskComment>.Success(taskComment);
    }
}
