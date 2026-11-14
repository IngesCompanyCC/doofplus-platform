namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a task comment.
/// </summary>
public record CreateTaskCommentResource(
    int TaskId,
    int AuthorId,
    string Content);
