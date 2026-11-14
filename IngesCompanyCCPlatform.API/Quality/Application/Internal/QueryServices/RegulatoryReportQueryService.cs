using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="RegulatoryReport" />.
/// </summary>
public class RegulatoryReportQueryService(IRegulatoryReportRepository regulatoryReportRepository) : IRegulatoryReportQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<RegulatoryReport>> Handle(GetAllRegulatoryReportsQuery query, CancellationToken cancellationToken)
    {
        return await regulatoryReportRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<RegulatoryReport?> Handle(GetRegulatoryReportByIdQuery query, CancellationToken cancellationToken)
    {
        return await regulatoryReportRepository.FindByIdAsNoTrackingAsync(query.RegulatoryReportId, cancellationToken);
    }
}
