namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to approve a batch release: the decision, the reference of the electronic signature and who signs.
/// </summary>
public record ApproveBatchReleaseResource(string Decision, string SignatureReference, string SignedBy);
