using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="TraceabilityGap" />.
/// </summary>
public class TraceabilityGapQueryService(ITraceabilityGapRepository traceabilityGapRepository) : ITraceabilityGapQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<TraceabilityGap>> Handle(GetAllTraceabilityGapsQuery query, CancellationToken cancellationToken)
    {
        return string.IsNullOrWhiteSpace(query.BatchCode)
            ? await traceabilityGapRepository.ListAsNoTrackingAsync(cancellationToken)
            : await traceabilityGapRepository.FindAllByBatchCodeAsNoTrackingAsync(query.BatchCode.Trim(), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TraceabilityGap?> Handle(GetTraceabilityGapByIdQuery query, CancellationToken cancellationToken)
    {
        return await traceabilityGapRepository.FindByIdAsNoTrackingAsync(query.TraceabilityGapId, cancellationToken);
    }
}
