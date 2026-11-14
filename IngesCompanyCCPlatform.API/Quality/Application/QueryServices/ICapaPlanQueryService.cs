using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Query service of <see cref="CapaPlan" />.
/// </summary>
public interface ICapaPlanQueryService
{
    /// <summary>
    ///     Gets all the capa plans.
    /// </summary>
    Task<IEnumerable<CapaPlan>> Handle(GetAllCapaPlansQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a capa plan by its id, or <c>null</c>.
    /// </summary>
    Task<CapaPlan?> Handle(GetCapaPlanByIdQuery query, CancellationToken cancellationToken);
}
