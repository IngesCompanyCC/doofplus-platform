using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Counts the deviations of each month from their detection date.
/// </summary>
public class DeviationTrendQueryService(IDeviationRepository deviationRepository) : IDeviationTrendQueryService
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<(string Month, int TotalDeviations)>> Handle(GetDeviationTrendsQuery query,
        CancellationToken cancellationToken)
    {
        var deviations = await deviationRepository.ListAsNoTrackingAsync(cancellationToken);
        return deviations
            .GroupBy(deviation => deviation.DetectedAt.ToString("yyyy-MM"))
            .OrderBy(group => group.Key)
            .Select(group => (group.Key, group.Count()))
            .ToList();
    }
}
