namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get the capa actions filtered by capa plan id.
/// </summary>
public record GetAllCapaActionsByCapaPlanIdQuery(int CapaPlanId);
