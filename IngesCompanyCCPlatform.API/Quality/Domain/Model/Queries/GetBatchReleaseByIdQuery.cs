namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a batch release by its id.
/// </summary>
/// <param name="BatchReleaseId">The id of the batch release.</param>
public record GetBatchReleaseByIdQuery(int BatchReleaseId);
