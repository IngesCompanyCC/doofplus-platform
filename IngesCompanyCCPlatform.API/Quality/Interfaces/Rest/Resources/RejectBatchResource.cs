namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Request resource to reject a batch under review.
/// </summary>
public record RejectBatchResource(string Reason, int SignatureId);
