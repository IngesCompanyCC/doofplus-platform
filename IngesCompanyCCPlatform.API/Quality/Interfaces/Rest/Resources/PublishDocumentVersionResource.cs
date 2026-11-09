namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Request resource to add a new version to an approved quality document.
/// </summary>
public record PublishDocumentVersionResource(string FileUrl, string ChangeReason, int AuthorId);
