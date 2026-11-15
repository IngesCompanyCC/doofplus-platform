namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query of the analytical results, optionally filtered by batch code.
/// </summary>
public record GetAllAnalyticalResultsQuery(string? BatchCode = null);
