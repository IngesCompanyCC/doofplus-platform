using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;

/// <summary>
///     Repository contract for the UserProfile aggregate.
/// </summary>
public interface IUserProfileRepository : IBaseRepository<UserProfile>
{
    Task<UserProfile?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<UserProfile?> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default);

    Task<IEnumerable<UserProfile>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
}