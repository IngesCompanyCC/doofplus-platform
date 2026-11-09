using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get an analytical result by its id.
/// </summary>
public record GetAnalyticalResultByIdQuery(AnalyticalResultId AnalyticalResultId);
