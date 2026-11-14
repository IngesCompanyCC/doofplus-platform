namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a collaboration task by its id.
/// </summary>
/// <param name="CollaborationTaskId">The id of the collaboration task.</param>
public record GetCollaborationTaskByIdQuery(int CollaborationTaskId);
