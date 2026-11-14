namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a capa plan by its id.
/// </summary>
/// <param name="CapaPlanId">The id of the capa plan.</param>
public record GetCapaPlanByIdQuery(int CapaPlanId);
