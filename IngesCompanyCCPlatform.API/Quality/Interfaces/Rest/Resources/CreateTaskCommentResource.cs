namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a task comment. Missing optional values take their default.
/// </summary>
public record CreateTaskCommentResource(
    string? RecordCode,
    string? Author,
    string? Initials,
    DateTime? PostedAt,
    string? Text);
