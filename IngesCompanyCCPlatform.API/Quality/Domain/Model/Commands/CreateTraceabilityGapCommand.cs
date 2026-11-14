using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a traceability gap.
/// </summary>
public record CreateTraceabilityGapCommand(
    int BatchId,
    string Description,
    string MissingReference,
    string AffectedProcess);
