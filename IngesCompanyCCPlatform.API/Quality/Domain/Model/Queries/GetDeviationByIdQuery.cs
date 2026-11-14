namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a deviation by its id.
/// </summary>
/// <param name="DeviationId">The id of the deviation.</param>
public record GetDeviationByIdQuery(int DeviationId);
