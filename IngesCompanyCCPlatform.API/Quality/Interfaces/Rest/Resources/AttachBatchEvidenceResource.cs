namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Request resource to attach an evidence file to a batch review.
/// </summary>
public record AttachBatchEvidenceResource(string FileUrl, string Description);
