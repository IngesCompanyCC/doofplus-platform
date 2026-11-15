using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="TraceabilityGap" />.
/// </summary>
public interface ITraceabilityGapRepository : IBaseRepository<TraceabilityGap>
{
    /// <summary>
    ///     Gets the traceability gaps with the batch code.
    /// </summary>
    Task<IEnumerable<TraceabilityGap>> FindAllByBatchCodeAsNoTrackingAsync(string batchCode, CancellationToken cancellationToken = default);
}
