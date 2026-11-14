namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A batch release, as the API returns it.
/// </summary>
public record BatchReleaseResource(
    int Id,
    int BatchId,
    string BatchCode,
    string RequestedBy,
    string Status,
    string Decision,
    string SignatureReference,
    string SignedBy,
    DateTime? SignedAt,
    DateTimeOffset? CreatedAt);
