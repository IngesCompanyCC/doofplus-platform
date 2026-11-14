namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a task comment by its id.
/// </summary>
/// <param name="TaskCommentId">The id of the task comment.</param>
public record GetTaskCommentByIdQuery(int TaskCommentId);
