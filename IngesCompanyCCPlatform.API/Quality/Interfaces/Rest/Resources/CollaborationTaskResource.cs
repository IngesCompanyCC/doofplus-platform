namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A collaboration task, with the fields of <c>contratos/quality-api.json</c>.
/// </summary>
public record CollaborationTaskResource(
    int Id,
    string Title,
    string RecordCode,
    string Detail,
    string Owner,
    DateTime? DueAt,
    string Status);
