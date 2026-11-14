using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Query service of <see cref="CapaAction" />.
/// </summary>
public interface ICapaActionQueryService
{
    /// <summary>
    ///     Gets all the capa actions.
    /// </summary>
    Task<IEnumerable<CapaAction>> Handle(GetAllCapaActionsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a capa action by its id, or <c>null</c>.
    /// </summary>
    Task<CapaAction?> Handle(GetCapaActionByIdQuery query, CancellationToken cancellationToken);
    /// <summary>
    ///     Gets the capa actions filtered by capa plan id.
    /// </summary>
    Task<IEnumerable<CapaAction>> Handle(GetAllCapaActionsByCapaPlanIdQuery query, CancellationToken cancellationToken);
}
