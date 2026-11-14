using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="CapaAction" />.
/// </summary>
public interface ICapaActionRepository : IBaseRepository<CapaAction>
{
    /// <summary>
    ///     Gets the capa actions filtered by capa plan id.
    /// </summary>
    Task<IEnumerable<CapaAction>> FindAllByCapaPlanIdAsNoTrackingAsync(int capaPlanId, CancellationToken cancellationToken = default);
}
