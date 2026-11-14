using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;

/// <summary>
///     Query service of the administrative activities.
/// </summary>
public interface IAdministrativeActivityQueryService
{
    Task<IEnumerable<AdministrativeActivity>> Handle(
        GetAllAdministrativeActivitiesQuery query,
        CancellationToken cancellationToken = default);
}
