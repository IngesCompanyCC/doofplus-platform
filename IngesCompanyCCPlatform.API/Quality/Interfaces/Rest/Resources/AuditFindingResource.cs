namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Response resource representing a finding or an observation of an audit.
/// </summary>
public record AuditFindingResource(int Id, string Type, string Description);
