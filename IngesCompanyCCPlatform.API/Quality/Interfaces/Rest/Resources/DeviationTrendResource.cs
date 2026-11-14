namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     The number of deviations registered in a month (<c>yyyy-MM</c>).
/// </summary>
public record DeviationTrendResource(string Month, int TotalDeviations);
