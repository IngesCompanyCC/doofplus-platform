namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Response resource representing a quality document with its versions.
/// </summary>
public record QualityDocumentResource(int Id, string Code, string Title, string Type, string Status, string CurrentVersion, IEnumerable<DocumentVersionResource> Versions);
