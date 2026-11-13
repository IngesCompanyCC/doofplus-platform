using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Errors;

/// <summary>
///     Domain errors of the subscription aggregate. Each code is the key of its localized message.
/// </summary>
public static class SubscriptionErrors
{
    public static readonly Error PlanNotFound =
        Error.NotFound("subscription.error.planNotFound", "The selected plan does not exist.");

    public static readonly Error AlreadySubscribed =
        Error.Conflict("subscription.error.alreadySubscribed",
            "The organization already has a pending or active subscription.");

    public static readonly Error NotFound =
        Error.NotFound("subscription.error.notFound", "Subscription not found.");

    public static Error NotFoundById(int id) =>
        Error.NotFound("subscription.error.notFoundById", $"Subscription not found for id {id}.");

    /// <summary>
    ///     Builds the error of a change of state that the subscription does not allow.
    /// </summary>
    /// <param name="messageKey">
    ///     The message key thrown by the aggregate, for example <c>subscription.error.state.notPending</c>.
    /// </param>
    public static Error InvalidState(string messageKey) =>
        Error.Validation(messageKey, "The subscription does not allow this change of state.");
}
