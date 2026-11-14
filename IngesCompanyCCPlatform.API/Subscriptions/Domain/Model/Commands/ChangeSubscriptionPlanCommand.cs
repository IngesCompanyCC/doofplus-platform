using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;

/// <summary>
///     Command to change the plan and the billing cycle of a pending or active subscription (US53).
/// </summary>
/// <param name="SubscriptionId">The subscription to change.</param>
/// <param name="PlanId">The new plan.</param>
/// <param name="BillingCycle">The new billing cycle.</param>
public record ChangeSubscriptionPlanCommand(SubscriptionId SubscriptionId, PlanId PlanId, EBillingCycle BillingCycle);
