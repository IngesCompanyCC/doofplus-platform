namespace IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Resources;

/// <summary>
///     Request resource used to select a plan for an organization (US51).
/// </summary>
/// <param name="OrganizationId">The organization identifier.</param>
/// <param name="PlanId">The plan identifier.</param>
/// <param name="BillingCycle">The billing cycle, <c>Monthly</c> or <c>Annual</c>.</param>
/// <param name="CardBrand">The card brand, for example <c>Visa</c>.</param>
/// <param name="CardLast4">The last four digits of the card.</param>
/// <param name="CardExpiry">The card expiry in <c>MM/yyyy</c> format.</param>
/// <param name="BillingEmail">The email that receives the invoices.</param>
public record CreateSubscriptionResource(
    int OrganizationId,
    int PlanId,
    string BillingCycle,
    string CardBrand,
    string CardLast4,
    string CardExpiry,
    string BillingEmail);
