using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Represents the regulatory report repository of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IRegulatoryReportRepository : IBaseRepository<RegulatoryReport>
{
    /// <summary>
    ///     Lists the regulatory reports of an organization, newest period first.
    /// </summary>
    /// <param name="organizationId">The organization.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The reports of the organization.</returns>
    Task<IReadOnlyCollection<RegulatoryReport>> FindByOrganizationIdAsync(OrganizationId organizationId,
        CancellationToken cancellationToken = default);
}
