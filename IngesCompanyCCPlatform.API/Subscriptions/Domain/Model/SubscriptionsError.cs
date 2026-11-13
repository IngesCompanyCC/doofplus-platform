namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model;

/// <summary>
///     Represents the error types of the Subscriptions bounded context.
/// </summary>
public enum SubscriptionsError
{
    None,
    DuplicatePlanKey,
    PlanNotFound,
    SubscriptionNotFound,
    AlreadySubscribed,
    InvalidSubscriptionState
}
