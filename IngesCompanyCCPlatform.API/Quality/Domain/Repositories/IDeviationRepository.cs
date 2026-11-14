using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="Deviation" />.
/// </summary>
public interface IDeviationRepository : IBaseRepository<Deviation>
{
    /// <summary>
    ///     Checks whether a deviation already has the code.
    /// </summary>
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
    /// <summary>
    ///     Gets the deviations filtered by batch id.
    /// </summary>
    Task<IEnumerable<Deviation>> FindAllByBatchIdAsNoTrackingAsync(int batchId, CancellationToken cancellationToken = default);
}
