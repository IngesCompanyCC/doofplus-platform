namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Response resource representing one version of a quality document.
/// </summary>
public record DocumentVersionResource(int Id, string VersionNumber, string FileUrl, string ChangeReason, int AuthorId, int? ApprovalSignatureId);
