namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query of the collaboration tasks, optionally filtered by record code.
/// </summary>
public record GetAllCollaborationTasksQuery(string? RecordCode = null);
