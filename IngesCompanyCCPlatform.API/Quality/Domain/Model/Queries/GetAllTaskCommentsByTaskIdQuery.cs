namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get the task comments filtered by task id.
/// </summary>
public record GetAllTaskCommentsByTaskIdQuery(int TaskId);
