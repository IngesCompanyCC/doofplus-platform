namespace IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Resources;

/// <summary>
///     Request resource used to change the plan and the billing cycle of a subscription.
/// </summary>
/// <param name="PlanId">The identifier of the new plan.</param>
/// <param name="BillingCycle">The new billing cycle, Monthly or Annual.</param>
public record ChangeSubscriptionPlanResource(int PlanId, string BillingCycle);
