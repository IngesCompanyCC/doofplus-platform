namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a quality document.
/// </summary>
public record CreateQualityDocumentResource(
    string Code,
    string Title,
    string? Description,
    string? DocumentType,
    string? FileReference,
    string? Version,
    int? OwnerId);
