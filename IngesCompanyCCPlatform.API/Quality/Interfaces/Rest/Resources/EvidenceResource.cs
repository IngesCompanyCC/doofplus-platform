namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A evidence, as the API returns it.
/// </summary>
public record EvidenceResource(
    int Id,
    string Title,
    string Description,
    string Type,
    string FileReference,
    int? DeviationId,
    int? DocumentId,
    int? UploadedBy,
    string Status,
    DateTime UploadedAt,
    DateTime? VerifiedAt);
