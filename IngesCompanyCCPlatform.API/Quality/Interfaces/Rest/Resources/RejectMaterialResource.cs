namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Request resource to reject a raw material lot.
/// </summary>
public record RejectMaterialResource(int DecidedBy, string Reason);
