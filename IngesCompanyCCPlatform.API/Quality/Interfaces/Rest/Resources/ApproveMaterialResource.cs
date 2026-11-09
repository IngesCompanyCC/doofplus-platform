namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Request resource to approve a sampled raw material lot.
/// </summary>
public record ApproveMaterialResource(int DecidedBy, int SignatureId);
