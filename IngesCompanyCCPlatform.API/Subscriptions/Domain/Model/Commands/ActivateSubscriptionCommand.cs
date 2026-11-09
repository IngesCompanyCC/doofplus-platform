using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;

/// <summary>
///     Command to activate a pending subscription once the payment is authorized (US52).
/// </summary>
/// <param name="SubscriptionId">
///     The ID of the subscription to activate.
/// </param>
public record ActivateSubscriptionCommand(SubscriptionId SubscriptionId)
{
    public ActivateSubscriptionCommand(int subscriptionId) : this(new SubscriptionId(subscriptionId))
    {
    }
}
