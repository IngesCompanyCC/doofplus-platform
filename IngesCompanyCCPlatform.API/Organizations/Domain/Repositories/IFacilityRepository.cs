using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;

/// <summary>
///     Repository contract for the Facility aggregate.
/// </summary>
public interface IFacilityRepository : IBaseRepository<Facility>
{
    Task<IEnumerable<Facility>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default);
}