namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Response resource representing an evidence attached to a batch review.
/// </summary>
public record EvidenceAttachmentResource(int Id, string FileUrl, string Description);
