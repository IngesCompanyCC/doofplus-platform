using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Represents the analytical result query service of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IAnalyticalResultQueryService
{
    Task<AnalyticalResult?> Handle(GetAnalyticalResultByIdQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<AnalyticalResult>> Handle(GetAnalyticalResultsByProductionBatchIdQuery query,
        CancellationToken cancellationToken);
}
