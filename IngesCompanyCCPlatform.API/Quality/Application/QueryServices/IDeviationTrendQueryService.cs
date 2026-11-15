using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Query service of the deviation trend, a read model built from the deviations.
/// </summary>
public interface IDeviationTrendQueryService
{
    /// <summary>
    ///     Counts the deviations by month of report (yyyy-MM) and root cause, oldest month first.
    /// </summary>
    Task<IReadOnlyList<(string Month, string RootCause, int Count)>> Handle(GetDeviationTrendsQuery query,
        CancellationToken cancellationToken);
}
