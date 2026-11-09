using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Implements the analytical result query service of the Quality &amp; Compliance bounded context.
/// </summary>
public class AnalyticalResultQueryService(IAnalyticalResultRepository analyticalResultRepository)
    : IAnalyticalResultQueryService
{
    /// <inheritdoc />
    public async Task<AnalyticalResult?> Handle(GetAnalyticalResultByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await analyticalResultRepository.FindByIdAsNoTrackingAsync(query.AnalyticalResultId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AnalyticalResult>> Handle(GetAnalyticalResultsByProductionBatchIdQuery query,
        CancellationToken cancellationToken)
    {
        return await analyticalResultRepository.FindByProductionBatchIdAsync(query.ProductionBatchId,
            cancellationToken);
    }
}
