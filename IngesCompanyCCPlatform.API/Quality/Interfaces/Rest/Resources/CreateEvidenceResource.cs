namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a evidence.
/// </summary>
public record CreateEvidenceResource(
    string Title,
    string? Description,
    string Type,
    string FileReference,
    int? DeviationId,
    int? DocumentId,
    int? UploadedBy);
