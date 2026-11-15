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
        return string.IsNullOrWhiteSpace(query.BatchCode)
            ? await analyticalResultRepository.ListAsNoTrackingAsync(cancellationToken)
            : await analyticalResultRepository.FindAllByBatchCodeAsNoTrackingAsync(query.BatchCode.Trim(), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<AnalyticalResult?> Handle(GetAnalyticalResultByIdQuery query, CancellationToken cancellationToken)
    {
        return await analyticalResultRepository.FindByIdAsNoTrackingAsync(query.AnalyticalResultId, cancellationToken);
    }
}
