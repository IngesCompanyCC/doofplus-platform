using IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Organizations.Application.Internal.QueryServices;

/// <summary>
///     Query service of the facilities.
/// </summary>
public class FacilityQueryService(IFacilityRepository facilityRepository) : IFacilityQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Facility>> Handle(GetAllFacilitiesQuery query, CancellationToken cancellationToken = default)
    {
        return query.OrganizationId is null
            ? await facilityRepository.ListAsNoTrackingAsync(cancellationToken)
            : await facilityRepository.FindByOrganizationIdAsync(query.OrganizationId.Value, cancellationToken);
    }
}
