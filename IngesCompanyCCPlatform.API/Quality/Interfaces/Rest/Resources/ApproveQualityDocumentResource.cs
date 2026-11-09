namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Request resource to approve the current version of a quality document.
/// </summary>
public record ApproveQualityDocumentResource(int ApproverId, int SignatureId);
