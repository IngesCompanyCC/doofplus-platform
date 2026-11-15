namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query of the capa plans, optionally filtered by source deviation.
/// </summary>
public record GetAllCapaPlansQuery(string? SourceDeviation = null);
