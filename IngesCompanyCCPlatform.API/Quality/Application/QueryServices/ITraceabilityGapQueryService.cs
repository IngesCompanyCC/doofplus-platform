using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Query service of <see cref="TraceabilityGap" />.
/// </summary>
public interface ITraceabilityGapQueryService
{
    /// <summary>
    ///     Gets the traceability gaps, filtered by batch code when it is sent.
    /// </summary>
    Task<IEnumerable<TraceabilityGap>> Handle(GetAllTraceabilityGapsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a traceability gap by its id, or <c>null</c>.
    /// </summary>
    Task<TraceabilityGap?> Handle(GetTraceabilityGapByIdQuery query, CancellationToken cancellationToken);
}
