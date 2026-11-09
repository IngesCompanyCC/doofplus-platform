namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Response resource representing a deviation with its CAPA actions.
/// </summary>
public record DeviationResource(int Id, string Code, int ProductionBatchId, string Severity, string Status, string Description, string? RootCauseMethod, string? RootCauseDescription, DateTime DetectedAt, DateTime? ClosedAt, IEnumerable<CapaActionResource> Actions);
