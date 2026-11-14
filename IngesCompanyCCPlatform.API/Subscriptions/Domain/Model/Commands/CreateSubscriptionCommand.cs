using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;

/// <summary>
///     Command to select a plan for an organization (US51). The subscription starts as pending.
/// </summary>
/// <param name="OrganizationId">
///     The organization that subscribes.
/// </param>
/// <param name="PlanId">
///     The ID of the selected plan.
/// </param>
/// <param name="BillingCycle">
///     The billing cycle, monthly or annual.
/// </param>
/// <param name="PaymentCard">
///     The card used to pay.
/// </param>
/// <param name="BillingEmail">
///     The email that receives the invoices.
/// </param>
public record CreateSubscriptionCommand(
    OrganizationId OrganizationId,
    PlanId PlanId,
    EBillingCycle BillingCycle,
    PaymentCard PaymentCard,
    BillingEmail BillingEmail);
