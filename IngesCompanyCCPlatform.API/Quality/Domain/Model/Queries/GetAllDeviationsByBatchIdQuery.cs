namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get the deviations filtered by batch id.
/// </summary>
public record GetAllDeviationsByBatchIdQuery(int BatchId);
