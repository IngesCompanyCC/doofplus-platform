namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a analytical result by its id.
/// </summary>
/// <param name="AnalyticalResultId">The id of the analytical result.</param>
public record GetAnalyticalResultByIdQuery(int AnalyticalResultId);
