namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;

/// <summary>
///     Command to cancel an active subscription (US53).
/// </summary>
/// <param name="SubscriptionId">
///     The ID of the subscription to cancel.
/// </param>
public record CancelSubscriptionCommand(int SubscriptionId);
