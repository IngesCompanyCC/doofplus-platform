using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Query service of <see cref="RegulatoryReport" />.
/// </summary>
public interface IRegulatoryReportQueryService
{
    /// <summary>
    ///     Gets all the regulatory reports.
    /// </summary>
    Task<IEnumerable<RegulatoryReport>> Handle(GetAllRegulatoryReportsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a regulatory report by its id, or <c>null</c>.
    /// </summary>
    Task<RegulatoryReport?> Handle(GetRegulatoryReportByIdQuery query, CancellationToken cancellationToken);
}
