using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a traceability gap.
/// </summary>
public record CreateTraceabilityGapCommand(
    string BatchCode,
    string Product,
    string MissingRecord,
    string Detail,
    EGapStatus Status);
