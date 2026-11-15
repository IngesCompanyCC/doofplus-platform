namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query of the deviations, optionally filtered by batch code.
/// </summary>
public record GetAllDeviationsQuery(string? BatchCode = null);
