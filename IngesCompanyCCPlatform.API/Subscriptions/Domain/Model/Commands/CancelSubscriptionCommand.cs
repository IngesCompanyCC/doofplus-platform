using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;

/// <summary>
///     Command to cancel an active subscription (US53).
/// </summary>
/// <param name="SubscriptionId">
///     The ID of the subscription to cancel.
/// </param>
public record CancelSubscriptionCommand(SubscriptionId SubscriptionId)
{
    public CancelSubscriptionCommand(int subscriptionId) : this(new SubscriptionId(subscriptionId))
    {
    }
}
