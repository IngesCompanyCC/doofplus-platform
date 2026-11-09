namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Request resource to register a deviation. Severity: Minor, Major or Critical.
/// </summary>
public record RegisterDeviationResource(string Code, int ProductionBatchId, string Severity, string Description);
