namespace IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Resources;

/// <summary>
///     Response resource representing a subscription.
/// </summary>
/// <param name="Id">The subscription identifier.</param>
/// <param name="OrganizationId">The organization identifier.</param>
/// <param name="PlanId">The plan identifier.</param>
/// <param name="BillingCycle">The billing cycle, <c>Monthly</c> or <c>Annual</c>.</param>
/// <param name="Status">The status, for example <c>Active</c>.</param>
/// <param name="AutoRenew">Whether the subscription renews automatically.</param>
/// <param name="CardBrand">The card brand.</param>
/// <param name="CardLast4">The last four digits of the card.</param>
/// <param name="CardExpiry">The card expiry.</param>
/// <param name="BillingEmail">The email that receives the invoices.</param>
/// <param name="CurrentPeriodEnd">The end of the current billing period, or <c>null</c> while pending.</param>
public record SubscriptionResource(
    int Id,
    int OrganizationId,
    int PlanId,
    string BillingCycle,
    string Status,
    bool AutoRenew,
    string CardBrand,
    string CardLast4,
    string CardExpiry,
    string BillingEmail,
    DateOnly? CurrentPeriodStart,
    DateOnly? CurrentPeriodEnd);
