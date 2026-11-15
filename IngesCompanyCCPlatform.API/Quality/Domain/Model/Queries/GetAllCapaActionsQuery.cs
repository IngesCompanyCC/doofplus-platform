namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query of the capa actions, optionally filtered by capa code.
/// </summary>
public record GetAllCapaActionsQuery(string? CapaCode = null);
