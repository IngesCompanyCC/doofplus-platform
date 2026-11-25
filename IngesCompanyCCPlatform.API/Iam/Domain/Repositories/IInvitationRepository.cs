using IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Iam.Domain.Repositories;

/// <summary>
///     Repository of <see cref="Invitation" />.
/// </summary>
public interface IInvitationRepository : IBaseRepository<Invitation>
{
    /// <summary>
    ///     Checks whether the email already has a pending invitation.
    /// </summary>
    Task<bool> ExistsPendingByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets the pending invitation of an email, or null when it has none.
    /// </summary>
    Task<Invitation?> FindPendingByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets the invitations of an organization.
    /// </summary>
    Task<IEnumerable<Invitation>> FindAllByOrganizationIdAsync(int organizationId, CancellationToken cancellationToken = default);
}
