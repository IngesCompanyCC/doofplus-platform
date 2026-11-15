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
    ///     Gets the deviations with the batch code.
    /// </summary>
    Task<IEnumerable<Deviation>> FindAllByBatchCodeAsNoTrackingAsync(string batchCode, CancellationToken cancellationToken = default);
}
