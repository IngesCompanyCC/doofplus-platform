using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a task comment.
/// </summary>
public record CreateTaskCommentCommand(
    int TaskId,
    int AuthorId,
    string Content);
