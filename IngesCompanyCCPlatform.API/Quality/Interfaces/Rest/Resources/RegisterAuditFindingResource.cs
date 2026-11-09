namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Request resource to register a finding or an observation. Type: Finding or Observation.
/// </summary>
public record RegisterAuditFindingResource(string Type, string Description);
