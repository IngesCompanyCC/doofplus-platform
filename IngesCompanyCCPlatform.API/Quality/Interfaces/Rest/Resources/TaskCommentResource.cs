namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A task comment, with the fields of <c>contratos/quality-api.json</c>.
/// </summary>
public record TaskCommentResource(
    int Id,
    string RecordCode,
    string Author,
    string Initials,
    DateTime? PostedAt,
    string Text);
