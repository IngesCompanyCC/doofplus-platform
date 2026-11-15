using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Builds the deviation trend from the deviations. Deviations without report date or root cause are not counted.
/// </summary>
public class DeviationTrendQueryService(IDeviationRepository deviationRepository) : IDeviationTrendQueryService
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<(string Month, string RootCause, int Count)>> Handle(GetDeviationTrendsQuery query,
        CancellationToken cancellationToken)
    {
        var deviations = await deviationRepository.ListAsNoTrackingAsync(cancellationToken);
        return deviations
            .Where(deviation => deviation.ReportedAt is not null && !string.IsNullOrWhiteSpace(deviation.RootCause))
            .GroupBy(deviation => (Month: deviation.ReportedAt!.Value.ToString("yyyy-MM"), deviation.RootCause))
            .OrderBy(group => group.Key.Month).ThenBy(group => group.Key.RootCause)
            .Select(group => (group.Key.Month, group.Key.RootCause, group.Count()))
            .ToList();
    }
}
