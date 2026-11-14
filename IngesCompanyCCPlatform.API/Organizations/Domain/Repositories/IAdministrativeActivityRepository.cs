using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;

/// <summary>
///     Repository of the administrative activities.
/// </summary>
public interface IAdministrativeActivityRepository : IBaseRepository<AdministrativeActivity>
{
    /// <summary>
    ///     Lists the activities, the most recent first.
    /// </summary>
    /// <param name="organizationId">The organization, or <c>null</c> for every activity.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The activities.</returns>
    Task<IEnumerable<AdministrativeActivity>> ListMostRecentFirstAsync(
        int? organizationId,
        CancellationToken cancellationToken = default);
}
