namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to approve a pending batch release with the electronic signature of Quality.
/// </summary>
public record ApproveBatchReleaseCommand(int ReleaseId, string Decision, string SignatureReference, string SignedBy);
