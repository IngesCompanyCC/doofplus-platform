using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;

/// <summary>
///     Command to renew an active subscription one more billing period.
/// </summary>
/// <param name="SubscriptionId">The subscription.</param>
public record RenewSubscriptionCommand(SubscriptionId SubscriptionId);
