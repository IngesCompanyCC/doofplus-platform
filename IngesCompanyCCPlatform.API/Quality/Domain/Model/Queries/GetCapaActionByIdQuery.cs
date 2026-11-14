namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a capa action by its id.
/// </summary>
/// <param name="CapaActionId">The id of the capa action.</param>
public record GetCapaActionByIdQuery(int CapaActionId);
