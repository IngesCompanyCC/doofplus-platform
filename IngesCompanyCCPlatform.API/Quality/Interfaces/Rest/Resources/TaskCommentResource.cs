namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A task comment, as the API returns it.
/// </summary>
public record TaskCommentResource(
    int Id,
    int TaskId,
    int AuthorId,
    string Content,
    DateTimeOffset? CreatedAt);
