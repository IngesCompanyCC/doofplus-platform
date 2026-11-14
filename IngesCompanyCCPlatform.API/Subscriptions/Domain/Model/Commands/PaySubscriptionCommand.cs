using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;

/// <summary>
///     Command to pay the next billing period of a subscription with a card tokenized by Niubiz.
/// </summary>
/// <param name="SubscriptionId">The subscription.</param>
/// <param name="CardToken">The card token created by the Niubiz payment form.</param>
public record PaySubscriptionCommand(SubscriptionId SubscriptionId, string CardToken);
