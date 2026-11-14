using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Transform;

/// <summary>
///     Converts a <see cref="CreateSubscriptionResource" /> into a <see cref="CreateSubscriptionCommand" />.
/// </summary>
public static class CreateSubscriptionCommandFromResourceAssembler
{
    private const string BillingCycleInvalidMessageKey = "subscription.error.billingCycle.invalid";

    public static CreateSubscriptionCommand ToCommandFromResource(CreateSubscriptionResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreateSubscriptionResource cannot be null when converting to command.");
        if (!Enum.TryParse<EBillingCycle>(resource.BillingCycle, true, out var billingCycle)
            || !Enum.IsDefined(billingCycle))
            throw new ArgumentException(BillingCycleInvalidMessageKey);
        return new CreateSubscriptionCommand(
            new OrganizationId(resource.OrganizationId),
            new PlanId(resource.PlanId),
            billingCycle,
            new PaymentCard(resource.CardBrand, resource.CardLast4, resource.CardExpiry),
            new BillingEmail(resource.BillingEmail));
    }
}
