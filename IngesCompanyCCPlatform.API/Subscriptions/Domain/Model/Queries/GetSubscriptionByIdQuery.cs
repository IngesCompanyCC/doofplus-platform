using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Queries;

/// <summary>
///     Represents a query to get a subscription by its id.
/// </summary>
/// <param name="SubscriptionId">
///     The id of the subscription to get.
/// </param>
public record GetSubscriptionByIdQuery(SubscriptionId SubscriptionId)
{
    public GetSubscriptionByIdQuery(int subscriptionId) : this(new SubscriptionId(subscriptionId))
    {
    }
}
