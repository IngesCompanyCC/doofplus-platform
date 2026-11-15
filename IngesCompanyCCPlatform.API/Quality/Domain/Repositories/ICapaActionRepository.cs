using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="CapaAction" />.
/// </summary>
public interface ICapaActionRepository : IBaseRepository<CapaAction>
{
    /// <summary>
    ///     Gets the capa actions with the capa code.
    /// </summary>
    Task<IEnumerable<CapaAction>> FindAllByCapaCodeAsNoTrackingAsync(string capaCode, CancellationToken cancellationToken = default);
}
