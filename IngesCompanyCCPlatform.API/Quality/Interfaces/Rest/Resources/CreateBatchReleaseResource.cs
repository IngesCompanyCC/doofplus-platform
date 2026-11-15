namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a batch release.
/// </summary>
public record CreateBatchReleaseResource(
    int BatchId,
    string BatchCode,
    string RequestedBy);
