using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Implements the regulatory report query service of the Quality &amp; Compliance bounded context.
/// </summary>
public class RegulatoryReportQueryService(IRegulatoryReportRepository regulatoryReportRepository)
    : IRegulatoryReportQueryService
{
    /// <inheritdoc />
    public async Task<RegulatoryReport?> Handle(GetRegulatoryReportByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await regulatoryReportRepository.FindByIdAsNoTrackingAsync(query.RegulatoryReportId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RegulatoryReport>> Handle(GetRegulatoryReportsByOrganizationIdQuery query,
        CancellationToken cancellationToken)
    {
        return await regulatoryReportRepository.FindByOrganizationIdAsync(query.OrganizationId, cancellationToken);
    }
}
