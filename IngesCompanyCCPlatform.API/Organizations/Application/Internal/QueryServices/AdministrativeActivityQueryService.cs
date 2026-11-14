using IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Organizations.Application.Internal.QueryServices;

/// <summary>
///     Query service of the administrative activities.
/// </summary>
public class AdministrativeActivityQueryService(IAdministrativeActivityRepository administrativeActivityRepository)
    : IAdministrativeActivityQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<AdministrativeActivity>> Handle(
        GetAllAdministrativeActivitiesQuery query,
        CancellationToken cancellationToken = default)
    {
        return await administrativeActivityRepository.ListMostRecentFirstAsync(query.OrganizationId, cancellationToken);
    }
}
