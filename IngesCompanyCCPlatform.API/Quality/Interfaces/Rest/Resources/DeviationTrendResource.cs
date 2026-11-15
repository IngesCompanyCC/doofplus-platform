namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Deviations of a root cause in a month (yyyy-MM).
/// </summary>
public record DeviationTrendResource(int Id, string RootCause, string Month, int Count);
