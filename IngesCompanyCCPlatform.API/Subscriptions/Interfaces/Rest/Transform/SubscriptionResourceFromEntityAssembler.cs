using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Transform;

/// <summary>
///     Converts a <see cref="Subscription" /> aggregate into a <see cref="SubscriptionResource" />.
/// </summary>
public static class SubscriptionResourceFromEntityAssembler
{
    public static SubscriptionResource ToResourceFromEntity(Subscription entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "Subscription entity cannot be null when converting to resource.");
        return new SubscriptionResource(
            entity.Id,
            entity.OrganizationId,
            entity.PlanId,
            entity.BillingCycle.ToString(),
            entity.Status.ToString(),
            entity.AutoRenew,
            entity.PaymentCard.Brand,
            entity.PaymentCard.Last4,
            entity.PaymentCard.Expiry,
            entity.BillingEmail.Value,
            entity.CurrentPeriodStart,
            entity.CurrentPeriodEnd,
            entity.Payments.Select(payment => new PaymentResource(payment.Id, payment.InvoiceNumber, payment.Amount,
                payment.Currency, payment.GatewayTransactionId, payment.Status.ToString(), payment.ProcessedAt)));
    }
}
