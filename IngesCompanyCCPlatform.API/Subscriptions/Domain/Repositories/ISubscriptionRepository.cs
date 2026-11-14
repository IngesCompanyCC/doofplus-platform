using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Repositories;

/// <summary>
///     Represents the subscription repository of the Subscriptions bounded context.
/// </summary>
public interface ISubscriptionRepository : IBaseRepository<Subscription>
{
    /// <summary>
    ///     Finds the subscriptions of an organization without change tracking.
    /// </summary>
    /// <param name="organizationId">
    ///     The id of the organization.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The subscriptions of the organization.
    /// </returns>
    Task<IEnumerable<Subscription>> FindAllByOrganizationIdAsNoTrackingAsync(OrganizationId organizationId,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Checks whether an organization has a subscription in any of the given states,
    ///     so that it keeps a single current subscription.
    /// </summary>
    /// <param name="organizationId">
    ///     The id of the organization.
    /// </param>
    /// <param name="statuses">
    ///     The states to look for.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     <c>true</c> when such a subscription exists.
    /// </returns>
    Task<bool> ExistsByOrganizationIdAndStatusInAsync(OrganizationId organizationId,
        IEnumerable<ESubscriptionStatus> statuses, CancellationToken cancellationToken = default);
}
