namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query of the task comments, optionally filtered by record code.
/// </summary>
public record GetAllTaskCommentsQuery(string? RecordCode = null);
