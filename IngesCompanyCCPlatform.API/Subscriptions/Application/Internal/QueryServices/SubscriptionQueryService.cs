using IngesCompanyCCPlatform.API.Subscriptions.Application.QueryServices;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Subscriptions.Application.Internal.QueryServices;

/// <summary>
///     Implements the subscription query service of the Subscriptions bounded context.
/// </summary>
public class SubscriptionQueryService(ISubscriptionRepository subscriptionRepository) : ISubscriptionQueryService
{
    /// <inheritdoc />
    public async Task<Subscription?> Handle(GetSubscriptionByIdQuery query, CancellationToken cancellationToken)
    {
        return await subscriptionRepository.FindByIdAsNoTrackingAsync(query.SubscriptionId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsByOrganizationIdQuery query,
        CancellationToken cancellationToken)
    {
        return await subscriptionRepository.FindAllByOrganizationIdAsNoTrackingAsync(query.OrganizationId,
            cancellationToken);
    }
}
