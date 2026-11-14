namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get the traceability gaps filtered by batch id.
/// </summary>
public record GetAllTraceabilityGapsByBatchIdQuery(int BatchId);
