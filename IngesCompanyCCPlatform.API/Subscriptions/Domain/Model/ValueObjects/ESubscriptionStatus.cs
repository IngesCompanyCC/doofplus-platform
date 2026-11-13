namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

/// <summary>
///     Represents the life cycle states of a subscription.
/// </summary>
public enum ESubscriptionStatus
{
    Pending,
    Active,
    Cancelled,
    Expired
}
