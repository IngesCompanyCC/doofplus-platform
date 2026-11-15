namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a collaboration task. Missing optional values take their default.
/// </summary>
public record CreateCollaborationTaskResource(
    string? Title,
    string? RecordCode,
    string? Detail,
    string? Owner,
    DateTime? DueAt,
    string? Status);
