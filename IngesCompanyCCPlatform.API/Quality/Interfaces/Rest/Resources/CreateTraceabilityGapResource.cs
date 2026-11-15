namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a traceability gap. Missing optional values take their default.
/// </summary>
public record CreateTraceabilityGapResource(
    string? BatchCode,
    string? Product,
    string? MissingRecord,
    string? Detail,
    string? Status);
