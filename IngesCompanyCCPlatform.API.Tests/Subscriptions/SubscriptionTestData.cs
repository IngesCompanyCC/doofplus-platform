using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Tests.Subscriptions;

/// <summary>
///     Builds the plans and subscriptions used by the Subscriptions tests.
/// </summary>
internal static class SubscriptionTestData
{
    public static Plan StandardLabPlan()
    {
        return new Plan(new CreatePlanCommand(new PlanKey("standard-lab"), "Standard Lab",
            new Money(199m, "USD"), new Money(1990m, "USD"), 10, 5));
    }

    public static Subscription PendingSubscription(EBillingCycle cycle = EBillingCycle.Monthly)
    {
        return new Subscription(new CreateSubscriptionCommand(new OrganizationId(1), new PlanId(1), cycle,
            new PaymentCard("Visa", "4242", "08/2028"), new BillingEmail("billing@andinos.com.pe")));
    }

    public static Subscription ActiveSubscription(EBillingCycle cycle = EBillingCycle.Monthly)
    {
        var subscription = PendingSubscription(cycle);
        subscription.Activate();
        return subscription;
    }
}
