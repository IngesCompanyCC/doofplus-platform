using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Query service of the monthly trend of deviations.
/// </summary>
public interface IDeviationTrendQueryService
{
    /// <summary>
    ///     Counts the deviations registered each month, oldest first.
    /// </summary>
    /// <returns>Pairs of month (<c>yyyy-MM</c>) and number of deviations.</returns>
    Task<IReadOnlyList<(string Month, int TotalDeviations)>> Handle(GetDeviationTrendsQuery query,
        CancellationToken cancellationToken);
}
