using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Represents the regulatory report query service of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IRegulatoryReportQueryService
{
    Task<RegulatoryReport?> Handle(GetRegulatoryReportByIdQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<RegulatoryReport>> Handle(GetRegulatoryReportsByOrganizationIdQuery query,
        CancellationToken cancellationToken);
}
