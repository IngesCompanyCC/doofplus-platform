namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to reject a batch release: the reason and who signs.
/// </summary>
public record RejectBatchReleaseResource(string Decision, string SignedBy);
