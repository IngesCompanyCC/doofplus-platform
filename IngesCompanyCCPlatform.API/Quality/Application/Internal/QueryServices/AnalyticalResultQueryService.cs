using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="AnalyticalResult" />.
/// </summary>
public class AnalyticalResultQueryService(IAnalyticalResultRepository analyticalResultRepository) : IAnalyticalResultQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<AnalyticalResult>> Handle(GetAllAnalyticalResultsQuery query, CancellationToken cancellationToken)
    {
        return await analyticalResultRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<AnalyticalResult?> Handle(GetAnalyticalResultByIdQuery query, CancellationToken cancellationToken)
    {
        return await analyticalResultRepository.FindByIdAsNoTrackingAsync(query.AnalyticalResultId, cancellationToken);
    }
    /// <inheritdoc />
    public async Task<IEnumerable<AnalyticalResult>> Handle(GetAllAnalyticalResultsByBatchIdQuery query, CancellationToken cancellationToken)
    {
        return await analyticalResultRepository.FindAllByBatchIdAsNoTrackingAsync(query.BatchId, cancellationToken);
    }
}
