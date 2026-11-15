using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="CapaPlan" />.
/// </summary>
public interface ICapaPlanRepository : IBaseRepository<CapaPlan>
{
    /// <summary>
    ///     Checks whether a capa plan already has the code.
    /// </summary>
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets the capa plans with the source deviation.
    /// </summary>
    Task<IEnumerable<CapaPlan>> FindAllBySourceDeviationAsNoTrackingAsync(string sourceDeviation, CancellationToken cancellationToken = default);
}
