namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get the batch releases filtered by batch id.
/// </summary>
public record GetAllBatchReleasesByBatchIdQuery(int BatchId);
