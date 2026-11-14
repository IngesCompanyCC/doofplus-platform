namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to reject a pending batch release.
/// </summary>
public record RejectBatchReleaseCommand(int ReleaseId, string Decision, string SignedBy);
