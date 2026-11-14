namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a traceability gap.
/// </summary>
public record CreateTraceabilityGapResource(
    int BatchId,
    string Description,
    string? MissingReference,
    string? AffectedProcess);
