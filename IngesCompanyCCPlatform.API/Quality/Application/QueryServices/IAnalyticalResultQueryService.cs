using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Query service of <see cref="AnalyticalResult" />.
/// </summary>
public interface IAnalyticalResultQueryService
{
    /// <summary>
    ///     Gets the analytical results, filtered by batch code when it is sent.
    /// </summary>
    Task<IEnumerable<AnalyticalResult>> Handle(GetAllAnalyticalResultsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a analytical result by its id, or <c>null</c>.
    /// </summary>
    Task<AnalyticalResult?> Handle(GetAnalyticalResultByIdQuery query, CancellationToken cancellationToken);
}
