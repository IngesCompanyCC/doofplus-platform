namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query of the traceability gaps, optionally filtered by batch code.
/// </summary>
public record GetAllTraceabilityGapsQuery(string? BatchCode = null);
