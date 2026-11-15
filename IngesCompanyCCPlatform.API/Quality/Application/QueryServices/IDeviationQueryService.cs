using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Query service of <see cref="Deviation" />.
/// </summary>
public interface IDeviationQueryService
{
    /// <summary>
    ///     Gets the deviations, filtered by batch code when it is sent.
    /// </summary>
    Task<IEnumerable<Deviation>> Handle(GetAllDeviationsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a deviation by its id, or <c>null</c>.
    /// </summary>
    Task<Deviation?> Handle(GetDeviationByIdQuery query, CancellationToken cancellationToken);
}
