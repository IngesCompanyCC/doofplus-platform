namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A quality document, as the API returns it.
/// </summary>
public record QualityDocumentResource(
    int Id,
    string Code,
    string Title,
    string Description,
    string DocumentType,
    string FileReference,
    string Version,
    int? OwnerId,
    string Status,
    DateTime? ApprovedAt);
