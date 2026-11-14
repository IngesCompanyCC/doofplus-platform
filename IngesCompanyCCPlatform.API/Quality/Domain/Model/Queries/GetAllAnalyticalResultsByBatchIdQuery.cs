namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get the analytical results filtered by batch id.
/// </summary>
public record GetAllAnalyticalResultsByBatchIdQuery(int BatchId);
