namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a traceability gap by its id.
/// </summary>
/// <param name="TraceabilityGapId">The id of the traceability gap.</param>
public record GetTraceabilityGapByIdQuery(int TraceabilityGapId);
