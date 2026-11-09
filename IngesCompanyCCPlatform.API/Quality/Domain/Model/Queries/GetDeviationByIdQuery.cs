using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a deviation by its id.
/// </summary>
public record GetDeviationByIdQuery(DeviationId DeviationId);
