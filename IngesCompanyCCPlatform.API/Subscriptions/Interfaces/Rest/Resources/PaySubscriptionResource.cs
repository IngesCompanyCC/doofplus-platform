namespace IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Resources;

/// <summary>
///     Request to pay the next billing period of a subscription.
/// </summary>
/// <param name="CardToken">The card token created by the Niubiz payment form.</param>
public record PaySubscriptionResource(string CardToken);
