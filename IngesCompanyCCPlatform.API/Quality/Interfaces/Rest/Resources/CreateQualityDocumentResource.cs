namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Request resource to create a quality document. Type: Protocol, Sop or Specification.
/// </summary>
public record CreateQualityDocumentResource(string Code, string Title, string Type, string FileUrl, int AuthorId);
